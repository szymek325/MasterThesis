import os

from configuration_global.paths_provider import PathsProvider
from domain.directory_manager import DirectoryManager
from dropbox_integration.dropbox_client import DropboxClient


class FilesUploader():
    def __init__(self):
        self.pathsProvider = PathsProvider()
        self.dropbox = DropboxClient()
        self.directory = DirectoryManager()

    def upload_detection_output(self, request_id: int, file: object):
        dropbox_save_location = os.join(self.pathsProvider.dropbox_detection_image_path(), request_id, file.name)
        self.dropbox.download_single_file(dropbox_save_location, file)
