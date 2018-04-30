import dropbox
import os
from dropbox_integration.configuration.config_reader import ConfigReader
from helpers.exception_handler import exception


class DropboxClient:

    def __init__(self):
        self.config = ConfigReader()
        self.client = dropbox.Dropbox(self.config.dropbox_access_token)

    @exception
    def get_file(self, request_id):
        folder_path = f"{self.config.face_detection_jobs_path}/{request_id}"
        save_directory = f"{self.config.temporary_files_path}/{request_id}"
        file = self.client.files_list_folder(folder_path).entries[0]
        file_type = file.name.split('.')[1]
        if not os.path.exists(save_directory):
            os.makedirs(save_directory)
        self.client.files_download_to_file(f"{save_directory}/input.{file_type}", file.path_lower)


if __name__ == "__main__":
    drop = DropboxClient()
    drop.get_file(3)
