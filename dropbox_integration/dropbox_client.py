import dropbox

from configuration_global.config_reader import ConfigReader
from domain.directory_manager import DirectoryManager
from configuration_global.exception_handler import exception


class DropboxClient:

    def __init__(self):
        self.config = ConfigReader()
        self.directory = DirectoryManager()
        self.client = dropbox.Dropbox("bJ90jq_k1TAAAAAAAAAABiHGq8c16qGnRew7tKYN1yJdChP3CRiPIpOG30ExjVkZ")

    def download_single_file(self, source_path: str, save_path):
        files = self.client.files_list_folder(f"/{source_path}")
        if not files.entries.count == 0:
            file = files.entries[0]
            self.client.files_download_to_file(f"{save_path}/{file.name}", file.path_lower)

    def download_folder(self, source_path: str, save_path):
        files = self.client.files_list_folder(f"/{source_path}")
        for file in files.entries:
            self.client.files_download_to_file(f"{save_path}/{file.name}", file.path_lower)

    def upload_file(self, path_with_file_name, file):
        self.client.files_upload(file, f"/{path_with_file_name}")
