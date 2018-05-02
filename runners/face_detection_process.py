import cv2
from sqlalchemy import null

from domain.directory_manager import DirectoryManager
from dataLayer.repositories.face_detection_repository import FaceDetectionRepository
from domain.face_detectors_manager import FaceDetectorsManager
from domain.temporary_files_janitor import TemporaryFilesJanitor
from dropbox_integration.dropbox_client import DropboxClient
from configuration_global.config_reader import ConfigReader
from configuration_global.exception_handler import exception
from configuration_global.logger_factory import LoggerFactory


class FaceDetectionProcess():
    def __init__(self):
        self.config = ConfigReader()
        self.janitor = TemporaryFilesJanitor()
        self.dbxClient = DropboxClient()
        self.faceDetectorsManager = FaceDetectorsManager()
        self.logger = LoggerFactory()
        self.requests_path = self.config.face_detection_requests_path
        self.directory = DirectoryManager()
        self.faceDetectionRepository = FaceDetectionRepository()

    @exception
    def run_face_detection(self):
        requests = self.faceDetectionRepository.get_all_not_completed()
        if not requests == null:
            for request in requests:
                self.logger.info(f"Working on Face Detection Request id: {request.id} started")
                self.dbxClient.download_face_detection_input(request.id, self.requests_path)

                image = cv2.imread(f"{self.requests_path}{request.id}/input.jpg")
                faces_detected_by_haar, faces_detected_by_dnn = self.faceDetectorsManager.get_faces_on_image(image)

                self.__draw_faces__(image, faces_detected_by_haar, faces_detected_by_dnn, request.id)
                self.__prepare_to_upload(request.id)
                self.faceDetectionRepository.complete_request(request.id, len(faces_detected_by_haar),
                                                              len(faces_detected_by_dnn))
                self.logger.info(f"Finished Face Detection Request id: {request.id} ")
        self.janitor.clean_face_detection_requests()
        
    def __draw_faces__(self, sourceImage, haarFaces, dnnFaces, id):
        haar = sourceImage.copy()
        dnn = sourceImage.copy()
        if len(haarFaces) is not 0:
            for face in haarFaces:
                startX, startY, endX, endY = face
                cv2.rectangle(haar, (startX, startY), (endX, endY), (0, 255, 0), 2)  # green
        if len(dnnFaces) is not 0:
            for face in dnnFaces:
                startX, startY, endX, endY = face
                cv2.rectangle(dnn, (startX, startY), (endX, endY), (0, 0, 255), 2)  # red
        save_path = f"{self.requests_path}{id}"
        self.directory.create_directory_if_doesnt_exist(save_path)
        cv2.imwrite(f"{save_path}/haar.png", haar)
        cv2.imwrite(f"{save_path}/dnn.png", dnn)

    def __prepare_to_upload(self, id):
        path = f"{self.requests_path}{id}"
        haar = open(f"{path}/haar.png", 'rb')
        dnn = open(f"{path}/dnn.png", 'rb')
        self.dbxClient.upload_file(haar.read(), id, "haar.png")
        self.dbxClient.upload_file(dnn.read(), id, "dnn.png")


if __name__ == "__main__":
    drop = FaceDetectionProcess()
    drop.run_face_detection()
