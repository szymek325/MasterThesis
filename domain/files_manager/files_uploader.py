import os

from configuration_global.paths_provider import PathsProvider
from dropbox_integration.dropbox_client import DropboxClient


class FilesUploader():
    def __init__(self):
        self.pathsProvider = PathsProvider()
        self.dropbox = DropboxClient()

    def upload_detection_output(self, request_id: int, file, file_name):
        dropbox_save_location = os.path.join(self.pathsProvider.dropbox_detection_image_path(), str(request_id), file_name).replace("\\","/")
        self.dropbox.download_single_file(dropbox_save_location, file)
