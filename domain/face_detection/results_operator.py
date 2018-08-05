import os

import cv2

from configuration_global.exception_handler import exception
from configuration_global.logger_factory import LoggerFactory
from configuration_global.paths_provider import PathsProvider
from dataLayer.entities.detection_rectangle import DetectionRectangle
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
        self.detectionTypes = DetectionTypes()
        self.resultsRepository = DetectionResultRepository()
        self.logger = LoggerFactory()

    @exception
    def prepare_and_upload_results(self, request_id: int, results, image_file_path):
        for res in results:
            type_name = res[0]
            file_name = f"{type_name}.jpg"
            faces = res[1]
            if faces is None:
                return
            self.logger.info(f"Working on results for {type_name} \n{faces}")
            result_file_path = os.path.join(self.pathsProvider.local_detection_image_path(), str(request_id), file_name)
            self.__save_result_image_to_local_directory__(faces, image_file_path, result_file_path)
            result_entity = self.__prepare_result_entities__(faces, file_name, request_id, type_name)
            result_id = self.resultsRepository.add_detection_result_with_image(result_entity)
            result_file = open(result_file_path, "rb")
            self.filesUploader.upload_detection_result(result_id, result_file.read(), file_name)

    def __prepare_result_entities__(self, faces, file_name, request_id, type_name):
        image_attachment = ImageAttachment(file_name, self.attachmentTypes.detection_result_id)
        faces_coordinates = [DetectionRectangle(faces) for faces in faces]
        result_entity = DetectionResult(request_id, self.detectionTypes.get_type_id(type_name), image_attachment, faces_coordinates)
        return result_entity

    def __save_result_image_to_local_directory__(self, faces, image_file_path, result_file_path):
        image = cv2.imread(image_file_path)
        result_image_data = self.imageEditor.draw_faces(image, faces)
        cv2.imwrite(result_file_path, result_image_data)
