from os import path, listdir

from configuration_global.config_reader import ConfigReader
from configuration_global.exception_handler import exception
from configuration_global.logger_factory import LoggerFactory
from opencv_client.face_detection.dnn_face_detector import DnnFaceDetector
from opencv_client.image_converters.image_converter import ImageConverter
from opencv_client.neural_network.neural_network_creator import NeuralNetworkCreator


class FaceRecognitionWorker():
    def __init__(self):
        self.logger = LoggerFactory()
        self.globalConfig = ConfigReader()
        self.faceDetector = DnnFaceDetector()
        self.neuralNetworkCreator = NeuralNetworkCreator()
        self.neuralNetworksPath = self.globalConfig.neural_networks_path
        self.imageConverter = ImageConverter()
        self.recognizer = "empty"

    @exception
    def recognize_face(self, image, neural_network_id):
        """
        :param neural_network_id: id of nn
        :param image: OPENCV IMAGE!!!
        :return: recognizedId
        """
        base_path = path.join(self.neuralNetworksPath, f"{neural_network_id}")
        files = [path.join(base_path, f) for f in listdir(base_path)]
        detected_faces = self.faceDetector.run_detector(image)
        print(f"number of faces detected : {len(detected_faces)}")
        result = []
        for file in files:
            self.recognizer = self.neuralNetworkCreator.create_neural_network(file)
            for (startX, startY, endX, endY) in detected_faces:
                predict_image = self.imageConverter.convert_to_pil_image(image[startY:endY, startX:endX])
                nbr_predicted, confidence = self.recognizer.predict(predict_image)
                result.append((nbr_predicted, confidence, [startX, startY, endX, endY]))
        return result
