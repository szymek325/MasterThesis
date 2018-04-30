import dropbox
import os
from dropbox_integration.configuration.config_reader import ConfigReader
from configuration_global.exception_handler import exception


class DropboxClient:

    def __init__(self):
        self.config = ConfigReader()
        self.client = dropbox.Dropbox(self.config.dropbox_access_token)

    @exception
    def download_face_detection_input(self, request_id, save_location):
        folder_path = f"{self.config.face_detection_jobs_path}/{request_id}"
        files = self.client.files_list_folder(folder_path)
        if not files.entries.count == 0:
            file = files.entries[0]
            file_type = file.name.split('.')[1]
            save_path = f"{save_location}{request_id}"
            self.create_directory_if_doesnt_exist(save_path)
            self.client.files_download_to_file(f"{save_path}/input.{file_type}", file.path_lower)

    @staticmethod
    def create_directory_if_doesnt_exist(save_directory):
        if not os.path.exists(save_directory):
            os.makedirs(save_directory)


if __name__ == "__main__":
    drop = DropboxClient()
    drop.download_face_detection_input(3)
