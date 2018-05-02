from sqlalchemy import null

from domain.directory_manager import DirectoryManager
from dataLayer.repositories.face_detection_repository import FaceDetectionRepository
from domain.face_detection_requests_manager import FaceDetectionRequestsManager
from configuration_global.exception_handler import exception


class FaceDetectionProcess():
    def __init__(self):
        self.request_manager = FaceDetectionRequestsManager()
        self.directory = DirectoryManager()
        self.faceDetectionRepository = FaceDetectionRepository()

    @exception
    def run_face_detection(self):
        requests = self.faceDetectionRepository.get_all_not_completed()
        if not requests == null:
            for request in requests:
                self.request_manager.process_request(request.id)
            self.directory.clean_face_detection_requests()


if __name__ == "__main__":
    drop = FaceDetectionProcess()
    drop.run_face_detection()
