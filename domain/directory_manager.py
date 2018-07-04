import os
import shutil

from configuration_global.exception_handler import exception
from configuration_global.paths_provider import PathsProvider


class DirectoryManager:
    def __init__(self):
        self.pathProvider= PathsProvider()

    @exception
    def create_directory_if_doesnt_exist(self, directory):
        if not os.path.exists(directory):
            os.makedirs(directory)

    @exception
    def clean_face_detection_requests(self):
        if os.path.exists(self.pathProvider.local_detection_image_path()):
            shutil.rmtree(self.pathProvider.local_detection_image_path())
