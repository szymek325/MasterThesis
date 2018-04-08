from helpers.config_reader import ConfigReader
from helpers.exception_handler import exception
from helpers.files_manager import FilesManager
from helpers.logger_factory import LoggerFactory
from image_operators.dnn_face_detector import DnnFaceDetector
from PIL import Image
import cv2
import numpy as np

from image_operators.haar_face_detector import HaarFaceDetector


class FaceRecognizer:
    def __init__(self):
        self.config = ConfigReader()
        self.filesManager = FilesManager()
        self.logger = LoggerFactory()
        self.faceDetector = DnnFaceDetector()
        self.recognizer = cv2.face.LBPHFaceRecognizer_create()
        self.recognizer.read(f"{self.config.openCv_files_path}faceRecognizer.yml")

    @exception
    def recognize_faces_on_image(self, image):
        """
        :param image: OPENCV IMAGE!!!
        :return: list of [recognizedID,confidencem,face location]
        """
        pilImage = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
        pilImage = Image.fromarray(pilImage).convert('L')
        predict_image = np.array(pilImage, 'uint8')
        faces = self.faceDetector.run_detector(image)
        print(f"number of faces detected : {len(faces)}")
        result = []
        for (startX, startY, endX, endY) in faces:
            nbr_predicted, conf = self.recognizer.predict(predict_image[startY:endY, startX:endX])
            result.append((nbr_predicted, conf, [startX, startY, endX, endY]))

    @exception
    def recognize_face_and_print_result(self, image, nbr_actual):
        """
        :param image: openCvImage
        :param nbr_actual: int
        :return: None
        """
        pilImage = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
        pilImage = Image.fromarray(pilImage).convert('L')
        predict_image = np.array(pilImage, 'uint8')
        faces = self.faceDetector.run_detector(image)
        print(f"number of faces detected : {len(faces)}")
        for (startX, startY, endX, endY) in faces:
            nbr_predicted, conf = self.recognizer.predict(predict_image[startY:endY, startX:endX])

            if nbr_actual == nbr_predicted:
                print("{} is Correctly Recognized with confidence {}".format(nbr_actual, conf))
            else:
                print("{} is Incorrectly Recognized as {}".format(nbr_actual, nbr_predicted))
            cv2.imshow("Recognizing Face", image[startY:endY, startX:endX])
            cv2.waitKey(5000)
            cv2.destroyAllWindows()
