import cv2
from datetime import datetime
import os
from configuration_global.logger_factory import LoggerFactory
from configuration_global.paths_provider import PathsProvider
from dataLayer.entities.image_attachment import ImageAttachment
from dataLayer.entities.notification import Notification
from dataLayer.repositories.notification_repository import NotificationRepository
from dataLayer.type_providers.image_attachment_types import ImageAttachmentTypes
from dataLayer.type_providers.notification_types import NotificationTypes
from domain.directory_manager import DirectoryManager
from dropbox_integration.files_uploader import FilesUploader
from opencv_client.image_converters.image_editor import ImageEditor

MOVEMENT_TIMESTAMP = False
MOVEMENT_MARKING = False


class MotionResultOperator:
    def __init__(self):
        self.imageEditor = ImageEditor()
        self.pathsProvider = PathsProvider()
        self.attachmentTypes = ImageAttachmentTypes()
        self.filesUploader = FilesUploader()
        self.notificationsRepo = NotificationRepository()
        self.notificationTypes = NotificationTypes()
        self.directoryManager = DirectoryManager()
        self.logger = LoggerFactory()

    def prepare_and_upload_result(self, frame, movements):
        self.logger.info("Motion detected")
        file_name = f"motion_{datetime.now().date()}.png"
        new_id = self.add_notification_to_db(file_name)
        file_path_to_create=os.path.join(self.pathsProvider.local_motion_image_path(), str(new_id))
        file_path = os.path.join(file_path_to_create, file_name)
        self.directoryManager.create_directory_if_doesnt_exist(file_path_to_create)
        self.__save_result_image_to_local_directory__(frame, movements, file_path)
        result_file = open(file_path, "rb")
        self.filesUploader.upload_detected_motion(new_id, result_file.read(), file_path)

    def add_notification_to_db(self, file_name):
        image_attachment = ImageAttachment(file_name, self.attachmentTypes.motion_id)
        notification_entity = Notification("Movement detected", self.notificationTypes.motion_detection,
                                           image_attachment)
        motion_id = self.notificationsRepo.add_movement_notification(notification_entity)
        return motion_id

    def __save_result_image_to_local_directory__(self, frame_to_upload, detected_movemenets, file_name):
        if MOVEMENT_TIMESTAMP:
            self.imageEditor.mark_frame(frame_to_upload, 'Occupied')
        if MOVEMENT_MARKING:
            self.imageEditor.draw_contour(detected_movemenets, frame_to_upload)
        cv2.imwrite(file_name, frame_to_upload)
