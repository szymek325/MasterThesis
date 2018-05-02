import cv2

from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from dataLayer.repositories.face_detection_repository import FaceDetectionRepository
from domain.directory_manager import DirectoryManager
from domain.face_detectors_manager import FaceDetectorsManager
from dropbox_integration.dropbox_client import DropboxClient


class FaceDetectionRequestsManager():
    def __init__(self):
        self.config = ConfigReader()
        self.dbxClient = DropboxClient()
        self.faceDetectorsManager = FaceDetectorsManager()
        self.logger = LoggerFactory()
        self.directory = DirectoryManager()
        self.faceDetectionRepository = FaceDetectionRepository()

        self.requests_path = self.config.face_detection_requests_path

    def process_request(self,request_id):
        self.logger.info(f"Working on Face Detection Request id: {request_id} started")
        self.dbxClient.download_face_detection_input(request_id, self.requests_path)

        image = cv2.imread(f"{self.requests_path}{request_id}/input.jpg")
        faces_detected_by_haar, faces_detected_by_dnn = self.faceDetectorsManager.get_faces_on_image(image)

        self.__draw_faces__(image, faces_detected_by_haar, faces_detected_by_dnn, request_id)
        self.__prepare_to_upload(request_id)
        self.faceDetectionRepository.complete_request(request_id, len(faces_detected_by_haar),
                                                      len(faces_detected_by_dnn))
        self.logger.info(f"Finished Face Detection Request id: {request_id} ")

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