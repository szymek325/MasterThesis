import os

import cv2

from configuration_global.paths_provider import PathsProvider
from dataLayer.entities.detection_result import DetectionResult
from dataLayer.entities.image_attachment import ImageAttachment
from dataLayer.repositories.detection_result_repository import DetectionResultRepository
from dataLayer.repositories.image_attachment_repository import ImageAttachmentRepository
from dataLayer.type_providers.detection_types import DetectionTypes
from dataLayer.type_providers.image_attachment_types import ImageAttachmentTypes
from dropbox_integration.files_uploader import FilesUploader
from opencv_client.image_converters.image_editor import ImageEditor


class ResultsOperator:
    def __init__(self):
        self.imageEditor = ImageEditor()
        self.filesUploader = FilesUploader()
        self.files_repository = ImageAttachmentRepository()
        self.pathsProvider = PathsProvider()
        self.attachmentTypes = ImageAttachmentTypes()
        self.detectionTypes=DetectionTypes()
        self.resultsRepository= DetectionResultRepository()
        self.haar_file_name = "haar.jpg"
        self.dnn_file_name = "dnn.jpg"

    def upload_results(self, request_id: int, faces_detected_by_dnn, faces_detected_by_haar, image):
        # TODO a lot of thinking how to resolve this shit
        haar_file_path, dnn_file_path = self.__prepare_results__(request_id, faces_detected_by_dnn,
                                                                 faces_detected_by_haar, image)
        haar_file = open(haar_file_path, "rb")
        dnn_file = open(dnn_file_path, "rb")
        if len(faces_detected_by_dnn) is not 0:
            dnn_image= ImageAttachment(self.dnn_file_name,self.attachmentTypes.detection_result_id)
            dnn_result=DetectionResult(faces_detected_by_dnn[0],request_id,self.detectionTypes.dnn_id,dnn_image)
            dnn_id=self.resultsRepository.add_detection_result_with_image(dnn_result)
            self.filesUploader.upload_detection_result(dnn_id, dnn_file.read(), self.dnn_file_name)
        if len(faces_detected_by_haar) is not 0:
            haar_image = ImageAttachment(self.haar_file_name, self.attachmentTypes.detection_result_id)
            haar_result=DetectionResult(faces_detected_by_haar[0],request_id,self.detectionTypes.haar_id,haar_image)
            haar_id=self.resultsRepository.add_detection_result_with_image(haar_result)
            self.filesUploader.upload_detection_result(haar_id, haar_file.read(), self.haar_file_name)
        # self.files_repository.add_detection_result_image(self.haar_file_name, request_id, self.attachmentTypes.detection_result_id)
        # self.files_repository.add_detection_result_image(self.dnn_file_name, request_id, self.attachmentTypes.detection_result_id)

    def __prepare_results__(self, request_id, dnn_faces, haar_faces, image):
        haar_file = self.imageEditor.draw_faces(image, haar_faces)
        dnn_file = self.imageEditor.draw_faces(image, dnn_faces)
        haar_path = os.path.join(self.pathsProvider.local_detection_image_path(), str(request_id), self.haar_file_name)
        cv2.imwrite(haar_path, haar_file)
        dnn_path = os.path.join(self.pathsProvider.local_detection_image_path(), str(request_id), self.dnn_file_name)
        cv2.imwrite(dnn_path, dnn_file)
        return haar_path, dnn_path
