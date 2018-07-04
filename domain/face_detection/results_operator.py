import cv2

from dataLayer.repositories.file_repository import FileRepository
from domain.files_manager.files_uploader import FilesUploader
from opencv_client.image_editor import ImageEditor


class ResultsOperator:
    def __init__(self):
        self.imageEditor = ImageEditor()
        self.filesUploader= FilesUploader()
        self.files_repository = FileRepository()

        self.haar_file_name = "haar.jpg"
        self.dnn_file_name = "dnn.jpg"

    def upload_results(self, request_id:int, faces_detected_by_dnn, faces_detected_by_haar, image):
        haar_file = self.imageEditor.draw_faces(image, faces_detected_by_haar)
        dnn_file = self.imageEditor.draw_faces(image, faces_detected_by_dnn)
        self.filesUploader.upload_detection_output(request_id,haar_file,self.haar_file_name)
        self.filesUploader.upload_detection_output(request_id, dnn_file, self.dnn_file_name)
        self.files_repository.add_detection_file(self.haar_file_name, request_id)
        self.files_repository.add_detection_file(self.dnn_file_name, request_id)
