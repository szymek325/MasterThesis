import os
import shutil

from configuration_global.config_reader import ConfigReader
from configuration_global.exception_handler import exception


class DirectoryManager:
    def __init__(self):
        self.config = ConfigReader()

    @exception
    def create_directory_if_doesnt_exist(self, directory):
        if not os.path.exists(directory):
            os.makedirs(directory)

    @exception
    def clean_face_detection_requests(self):
        if os.path.exists(self.config.face_detection_requests_path):
            shutil.rmtree(self.config.face_detection_requests_path)
