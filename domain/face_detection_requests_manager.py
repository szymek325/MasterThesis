import cv2

from configuration_global.config_reader import ConfigReader
from configuration_global.exception_handler import exception
from configuration_global.logger_factory import LoggerFactory
from dataLayer.entities.detection import Detection
from dataLayer.repositories.face_detection_repository import FaceDetectionRepository
from dataLayer.repositories.file_repository import FileRepository
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
        self.files_repository = FileRepository()
        self.requests_path = self.config.face_detection_requests_path
        self.haar_file_name = "haar.jpg"
        self.dnn_file_name = "dnn.jpg"
        self.dropbox_base_path = "DetectionImage"

    @exception
    def process_request(self, request: Detection):
        self.logger.info(f"Working on Face Detection Request id: {request.id} started")
        input_file = self.dbxClient.download_single_file_from_folder(f"{self.dropbox_base_path}/{request.id}", self.requests_path)
        image = cv2.imread(f'{self.requests_path}{self.dropbox_base_path}/{request.id}/{input_file}')
        faces_detected_by_haar, faces_detected_by_dnn = self.faceDetectorsManager.get_faces_on_image(image)
        save_path = f"{self.requests_path}{self.dropbox_base_path}/{request.id}"
        self.__prepare_results__(save_path, faces_detected_by_dnn, faces_detected_by_haar, image)
        self.__upload_results__(save_path, f"{self.dropbox_base_path}/{request.id}", request.id)
        self.faceDetectionRepository.complete_request(request.id, len(faces_detected_by_haar),
                                                          len(faces_detected_by_dnn))
        self.logger.info(f"Finished Face Detection Request id: {request.id} ")

    def __prepare_results__(self, save_path, faces_detected_by_dnn, faces_detected_by_haar, image):
        haar_file = self.__draw_faces__(image, faces_detected_by_haar)
        dnn_file = self.__draw_faces__(image, faces_detected_by_dnn)
        self.directory.create_directory_if_doesnt_exist(save_path)
        cv2.imwrite(f"{save_path}/{self.haar_file_name}", haar_file)
        cv2.imwrite(f"{save_path}/{self.dnn_file_name}", dnn_file)

    def __draw_faces__(self, source_image, faces):
        new_image = source_image.copy()
        if len(faces) is not 0:
            for face in faces:
                startX, startY, endX, endY = face
                cv2.rectangle(new_image, (startX, startY), (endX, endY), (0, 255, 0), 2)  # green
        return new_image

    def __upload_results__(self, path_to_files, request_guid, request_id):
        haar_file = open(f"{path_to_files}/{self.haar_file_name}", 'rb')
        dnn_file = open(f"{path_to_files}/{self.dnn_file_name}", 'rb')
        self.dbxClient.upload_file(haar_file.read(), request_guid, self.haar_file_name)
        self.dbxClient.upload_file(dnn_file.read(), request_guid, self.dnn_file_name)
        self.files_repository.add_detection_file(self.haar_file_name, request_id)
        self.files_repository.add_detection_file(self.dnn_file_name, request_id)
