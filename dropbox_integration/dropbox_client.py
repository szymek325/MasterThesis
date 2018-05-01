import dropbox
import os

from configuration_global.directory_manager import DirectoryManager
from dropbox_integration.configuration.config_reader import ConfigReader
from configuration_global.exception_handler import exception


class DropboxClient:

    def __init__(self):
        self.config = ConfigReader()
        self.directory = DirectoryManager()
        self.client = dropbox.Dropbox(self.config.dropbox_access_token)

    @exception
    def download_face_detection_input(self, request_id, save_location):
        folder_path = f"{self.config.face_detection_jobs_path}/{request_id}"
        files = self.client.files_list_folder(folder_path)
        if not files.entries.count == 0:
            file = files.entries[0]
            file_type = file.name.split('.')[1]
            save_path = f"{save_location}{request_id}"
            self.directory.create_directory_if_doesnt_exist(save_path)
            self.client.files_download_to_file(f"{save_path}/input.{file_type}", file.path_lower)

    def upload_file(self, file, request_id,file_name):
        self.client.files_upload(file, f"{self.config.face_detection_jobs_path}/{request_id}/{file_name}")


if __name__ == "__main__":
    drop = DropboxClient()
    drop.download_face_detection_input(3)
