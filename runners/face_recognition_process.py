from sqlalchemy import null

from configuration_global.config_reader import ConfigReader
from configuration_global.exception_handler import exception
from configuration_global.logger_factory import LoggerFactory
from dataLayer.repositories.face_recognition_repository import FaceRecognitionRepository
from domain.face_recognition.face_recognition_requests_manager import FaceRecognitionRequestManager
from domain.neural_network.neural_networks_manager import NeuralNetworksManager


class FaceRecognitionProcess():
    def __init__(self):
        self.logger = LoggerFactory()
        self.config = ConfigReader()
        self.faceRecognitionManager = FaceRecognitionRequestManager()
        self.faceRecognitionRepo = FaceRecognitionRepository()
        self.neuralNetworksMannager = NeuralNetworksManager()

    @exception
    def run_face_recognition(self):
        self.neuralNetworksMannager.download_neural_networks_to_local()
        requests = self.faceRecognitionRepo.get_all_not_completed()
        if not requests == null:
            for request in requests:
                self.faceRecognitionManager.process_request(request)
        self.logger.info("Processing done")


if __name__ == "__main__":
    drop = FaceRecognitionProcess()
    drop.run_face_recognition()
