import cv2

from configuration_global.logger_factory import LoggerFactory
from opencv_client.face_recognition.face_recognizer_names import FaceRecognizerNames


class NeuralNetworkProvider():
    def __init__(self):
        self.logger = LoggerFactory()
        self.recognizerNames = FaceRecognizerNames()
        self.recognizer = "empty"

    def create_neural_network_by_type(self, file_path: str, nn_type: str):
        self.logger.info(f"Creating FaceRecognizer of type: {nn_type}")
        if nn_type.lower() == self.recognizerNames.eigen():
            self.recognizer = cv2.face.EigenFaceRecognizer_create()
        elif nn_type.lower() == self.recognizerNames.lbph():
            self.recognizer = cv2.face.LBPHFaceRecognizer_create()
        elif nn_type.lower() == self.recognizerNames.fisher():
            self.recognizer = cv2.face.FisherFaceRecognizer_create()
        self.recognizer.read(file_path)
        return self.recognizer
