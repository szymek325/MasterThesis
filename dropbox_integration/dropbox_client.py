import dropbox

from domain.directory_manager import DirectoryManager
from dropbox_integration.configuration.config_reader import ConfigReader
from configuration_global.exception_handler import exception


class DropboxClient:

    def __init__(self):
        self.config = ConfigReader()
        self.directory = DirectoryManager()
        self.client = dropbox.Dropbox(self.config.dropbox_access_token)

    @exception
    def download_face_detection_input(self, guid, save_location):
        folder_path = f"{self.config.base_path}/{guid}"
        files = self.client.files_list_folder(folder_path)
        if not files.entries.count == 0:
            file = files.entries[0]
            save_path = f"{save_location}{guid}"
            self.directory.create_directory_if_doesnt_exist(save_path)
            self.client.files_download_to_file(f"{save_path}/{file.name}", file.path_lower)
            return file.name

    @exception
    def download_folder(self, guid, save_location):
        folder_path = f"{self.config.base_path}/{guid}"
        files = self.client.files_list_folder(folder_path)
        for file in files.entries:
            self.directory.create_directory_if_doesnt_exist(save_location)
            self.client.files_download_to_file(f"{save_location}/{file.name}", file.path_lower)
        # if not files.entries.count == 0:
        #     file = files.entries[0]
        #     save_path = f"{save_location}{guid}"
        #     self.directory.create_directory_if_doesnt_exist(save_path)
        #     self.client.files_download_to_file(f"{save_path}/{file.name}", file.path_lower)
        #     return file.name

    def upload_file(self, file, guid, file_name):
        self.client.files_upload(file, f"{self.config.base_path}/{guid}/{file_name}")


if __name__ == "__main__":
    drop = DropboxClient()
    drop.download_face_detection_input(3)
