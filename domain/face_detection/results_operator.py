import cv2

from dataLayer.repositories.file_repository import FileRepository
from domain.directory_manager import DirectoryManager
from dropbox_integration.dropbox_client import DropboxClient
from opencv_client.image_editor import ImageEditor


class ResultsOperator:
    def __init__(self):
        self.directory = DirectoryManager()
        self.imageEditor = ImageEditor()
        self.dbxClient = DropboxClient()
        self.files_repository = FileRepository()
        self.haar_file_name = "haar.jpg"
        self.dnn_file_name = "dnn.jpg"

    def prepare_results(self, save_path, faces_detected_by_dnn, faces_detected_by_haar, image):
        haar_file = self.imageEditor.draw_faces(image, faces_detected_by_haar)
        dnn_file = self.imageEditor.draw_faces(image, faces_detected_by_dnn)
        self.directory.create_directory_if_doesnt_exist(save_path)
        cv2.imwrite(f"{save_path}/{self.haar_file_name}", haar_file)
        cv2.imwrite(f"{save_path}/{self.dnn_file_name}", dnn_file)

    def upload_results(self, path_to_files, request_guid, request_id):
        haar_file = open(f"{path_to_files}/{self.haar_file_name}", 'rb')
        dnn_file = open(f"{path_to_files}/{self.dnn_file_name}", 'rb')
        self.dbxClient.upload_file(haar_file.read(), request_guid, self.haar_file_name)
        self.dbxClient.upload_file(dnn_file.read(), request_guid, self.dnn_file_name)
        self.files_repository.add_detection_file(self.haar_file_name, request_id)
        self.files_repository.add_detection_file(self.dnn_file_name, request_id)
