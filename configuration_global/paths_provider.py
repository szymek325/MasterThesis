import os

from configuration_global.config_reader import ConfigReader
from dataLayer.type_providers.image_attachment_types import ImageAttachmentTypes


class PathsProvider():
    def __init__(self):
        self.config = ConfigReader()
        self.imageAttachmentTypes=ImageAttachmentTypes()

    def dropbox_person_image_path(self):
        return os.path.join(self.config.environment_name(), self.imageAttachmentTypes.person)

    def dropbox_detection_image_path(self):
        return os.path.join(self.config.environment_name(), self.imageAttachmentTypes.detection)

    def dropbox_detection_result_image_path(self):
        return os.path.join(self.config.environment_name(), self.imageAttachmentTypes.detection_result)

    def dropbox_recognition_image_path(self):
        return os.path.join(self.config.environment_name(), self.imageAttachmentTypes.recognition)

    def dropbox_neural_network_path(self):
        return os.path.join(self.config.environment_name(), "NeuralNetworks")

    def local_person_image_path(self):
        return os.path.join(self.config.local_files_path, self.dropbox_person_image_path())

    def local_detection_image_path(self):
        return os.path.join(self.config.local_files_path, self.dropbox_detection_image_path())

    def local_detection_result_image_path(self):
        return os.path.join(self.config.local_files_path, self.dropbox_detection_result_image_path())

    def local_recognition_image_path(self):
        return os.path.join(self.config.local_files_path, self.dropbox_recognition_image_path())

    def local_neural_network_path(self):
        return os.path.join(self.config.local_files_path, self.dropbox_neural_network_path())
