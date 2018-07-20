import os

from configuration_global.config_reader import ConfigReader


class PathsProvider():
    def __init__(self):
        self.config = ConfigReader()

    def dropbox_person_image_path(self):
        return os.path.join(self.config.environment_name(), "PersonImage")

    def dropbox_detection_image_path(self):
        return os.path.join(self.config.environment_name(), "DetectionImage")

    def dropbox_detection_result_image_path(self):
        return os.path.join(self.config.environment_name(), "DetectionResultImage")

    def dropbox_recognition_image_path(self):
        return os.path.join(self.config.environment_name(), "RecognitionImage")

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
