import os

from configuration_global.paths_provider import PathsProvider
from domain.directory_manager import DirectoryManager
from dropbox_integration.dropbox_client import DropboxClient


class FilesDownloader():
    def __init__(self):
        self.pathsProvider = PathsProvider()
        self.dropbox = DropboxClient()
        self.directory = DirectoryManager()

    def download_detection_input(self, request_id: int):
        dropbox_request_path = os.path.join(self.pathsProvider.dropbox_detection_image_path(), str(request_id)).replace("\\","/")
        local_save_path = os.path.join(self.pathsProvider.local_detection_image_path(), str(request_id))
        self.directory.create_directory_if_doesnt_exist(local_save_path)
        downloaded_file_name = self.dropbox.download_single_file(dropbox_request_path, local_save_path)
        return os.path.join(local_save_path, downloaded_file_name)
