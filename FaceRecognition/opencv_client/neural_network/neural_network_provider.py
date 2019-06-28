import cv2

from configuration_global.logger_factory import LoggerFactory
from dataLayer.type_providers.neural_network_types import NeuralNetworkTypes


class NeuralNetworkProvider():
    def __init__(self):
        self.logger = LoggerFactory()
        self.nnTypes = NeuralNetworkTypes()
        self.recognizer = "empty"

    def create_neural_network_by_type_id(self, file_path: str, nn_type_id: str):
        self.logger.info(f"Creating FaceRecognizer of type: {nn_type_id}")
        if nn_type_id == self.nnTypes.eigen_id:
            self.recognizer = cv2.face.EigenFaceRecognizer_create()
        elif nn_type_id == self.nnTypes.lbph_id:
            self.recognizer = cv2.face.LBPHFaceRecognizer_create()
        elif nn_type_id == self.nnTypes.fisher_id:
            self.recognizer = cv2.face.FisherFaceRecognizer_create()
        self.recognizer.read(file_path)
        return self.recognizer
