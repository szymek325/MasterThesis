from os import path, listdir

import cv2
from PIL import Image
import numpy as np
from configuration_global.config_reader import ConfigReader
from configuration_global.exception_handler import exception
from configuration_global.logger_factory import LoggerFactory
from faceDetection.dnn_face_detector import DnnFaceDetector


class FaceRecognitionWorker():
    def __init__(self):
        self.logger = LoggerFactory()
        self.globalConfig = ConfigReader()
        self.faceDetector = DnnFaceDetector()
        self.neuralNetworksPath = self.globalConfig.neural_networks_path
        self.recognizer = "empty"

    @exception
    def recognize_face(self, image, neural_network_id):
        """
        :param image: OPENCV IMAGE!!!
        :return: recognizedId
        """
        width_d, height_d = 280, 280  # Declare your own width and height
        image = cv2.resize(image, (width_d, height_d))

        base_path = path.join(self.neuralNetworksPath, f"{neural_network_id}")
        files = [path.join(base_path, f) for f in listdir(base_path)]

        # pilImage = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
        # pilImage = Image.fromarray(pilImage).convert('L')
        # predict_image = np.array(pilImage, 'uint8')

        faces = self.faceDetector.run_detector(image)
        print(f"number of faces detected : {len(faces)}")
        result = []
        for file in files:
            self.__create_neural_network__(file)
            for (startX, startY, endX, endY) in faces:

                face = cv2.resize(image[startY:endY, startX:endX], (width_d, height_d))
                pilImage = cv2.cvtColor(face, cv2.COLOR_BGR2RGB)
                pilImage = Image.fromarray(pilImage).convert('L')
                predict_image = np.array(pilImage, 'uint8')

                nbr_predicted, conf = self.recognizer.predict(predict_image)
                result.append((nbr_predicted, conf, [startX, startY, endX, endY]))
            return result

    def __create_neural_network__(self, file_path):
        file_name = file_path.split('\\')[-1]
        nn_type = file_name.split('_')[1].split('.')[0].lower()
        self.logger.info(file_path)
        self.logger.info(nn_type)
        if nn_type == 'eigen':
            self.recognizer = cv2.face.EigenFaceRecognizer_create()
        elif nn_type == 'lbph':
            self.recognizer = cv2.face.LBPHFaceRecognizer_create()
        elif nn_type == 'fisher':
            self.recognizer = cv2.face.FisherFaceRecognizer_create()
        self.recognizer.read(file_path)
