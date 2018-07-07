from sqlalchemy import null

from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from domain.directory_manager import DirectoryManager
from dataLayer.repositories.face_detection_repository import FaceDetectionRepository
from domain.face_detection.face_detection_requests_manager import FaceDetectionRequestsManager
from configuration_global.exception_handler import exception


class FaceDetectionProcess():
    def __init__(self):
        self.logger = LoggerFactory()
        self.config = ConfigReader()
        self.request_manager = FaceDetectionRequestsManager()
        self.directory = DirectoryManager()
        self.faceDetectionRepository = FaceDetectionRepository()

    @exception
    def run_face_detection(self):
        requests = self.faceDetectionRepository.get_all_not_completed()
        self.logger.info("START FaceDetection")
        if not requests == null:
            for request in requests:
                self.request_manager.process_request(request)
            self.directory.clean_face_detection_requests()
        self.logger.info("END FaceDetection")


if __name__ == "__main__":
    drop = FaceDetectionProcess()
    drop.run_face_detection()
