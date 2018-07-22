import traceback

from sqlalchemy import null

from configuration_global.config_reader import ConfigReader
from configuration_global.exception_handler import exception
from configuration_global.logger_factory import LoggerFactory
from dataLayer.repositories.face_recognition_repository import FaceRecognitionRepository
from domain.face_recognition.recognition_requests_manager import RecognitionRequestManager
from domain.neural_network.neural_networks_manager import NeuralNetworksManager


class FaceRecognitionProcess():
    def __init__(self):
        self.logger = LoggerFactory()
        self.config = ConfigReader()
        self.faceRecognitionManager = RecognitionRequestManager()
        self.faceRecognitionRepo = FaceRecognitionRepository()
        self.neuralNetworksManager = NeuralNetworksManager()

    @exception
    def run_face_recognition(self):
        self.logger.info("  START FaceRecognition")
        requests = self.faceRecognitionRepo.get_all_not_completed()
        if not requests == null and requests.count() is not 0:
            self.neuralNetworksManager.download_neural_networks_to_local()
            for request in requests:
                try:
                    self.faceRecognitionManager.process_request(request)
                except Exception as ex:
                    self.logger.error(f"Exception when processing recognition {request.id}.\n Error: {str(ex)}")
                    self.faceRecognitionRepo.complete_with_error(request.id)
        self.logger.info("  END FaceRecognition")


if __name__ == "__main__":
    drop = FaceRecognitionProcess()
    drop.run_face_recognition()
