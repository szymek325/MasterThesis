from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from dataLayer.entities.face_recognition import FaceRecognition


class FaceRecognitionRequestManager():
    def __init__(self):
        self.logger = LoggerFactory()
        self.config = ConfigReader()

    def process_request(self, request: FaceRecognition):
        self.logger.info(f"Working on face recognition request {request.id} id")
        self.logger.info(request.neural_network.name)
