from sqlalchemy import null

from configuration_global.config_reader import ConfigReader
from configuration_global.exception_handler import exception
from configuration_global.logger_factory import LoggerFactory
from configuration_global.paths_provider import PathsProvider
from domain.directory_manager import DirectoryManager
from dataLayer.repositories.face_detection_repository import FaceDetectionRepository
from domain.face_detection.detection_requests_manager import DetectionRequestsManager


class FaceDetectionProcess():
    def __init__(self):
        self.logger = LoggerFactory()
        self.config = ConfigReader()
        self.request_manager = DetectionRequestsManager()
        self.directory = DirectoryManager()
        self.faceDetectionRepository = FaceDetectionRepository()
        self.pathsProvider = PathsProvider()

    @exception
    def run_face_detection(self):
        self.logger.info("  START FaceDetection")
        requests = self.faceDetectionRepository.get_all_not_completed()
        if not requests == null:
            for request in requests:
                try:
                    self.request_manager.process_request(request)
                except Exception as ex:
                    self.logger.error(f"Exception when processing detection {request.id}. Error: \n {str(ex)}")
                    self.faceDetectionRepository.complete_with_error(request.id)
            self.directory.clean_directory(self.pathsProvider.local_detection_image_path())
        self.logger.info("  END FaceDetection")


if __name__ == "__main__":
    drop = FaceDetectionProcess()
    drop.run_face_detection()
