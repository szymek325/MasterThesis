import os

import cv2

from configuration_global.logger_factory import LoggerFactory
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
        self.detectionTypes = DetectionTypes()
        self.resultsRepository = DetectionResultRepository()
        self.logger = LoggerFactory()

    def upload_results(self, request_id: int, results, image_file_path):
        for res in results:
            type_name = res[0]
            faces = res[1]
            self.logger.info(res)
            image = cv2.imread(image_file_path)
            result_image_data = self.imageEditor.draw_faces(image, faces)
            file_name = f"{type_name}.jpg"
            result_file_path = os.path.join(self.pathsProvider.local_detection_image_path(), str(request_id), file_name)
            cv2.imwrite(result_file_path, result_image_data)
            image_attachment = ImageAttachment(file_name, self.attachmentTypes.detection_result_id)
            main_face_coordinates = faces[0] if len(faces) != 0 else [0, 0, 0, 0]
            result_entity = DetectionResult(main_face_coordinates[0], request_id,
                                            self.detectionTypes.get_type_id(type_name), image_attachment)
            result_id = self.resultsRepository.add_detection_result_with_image(result_entity)
            result_file = open(result_file_path, "rb")
            self.filesUploader.upload_detection_result(result_id, result_file.read(), file_name)
