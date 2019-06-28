import os

from configuration_global.paths_provider import PathsProvider
from domain.directory_manager import DirectoryManager
from dropbox_integration.files_downloader import FilesDownloader


class InputFileProvider():
    def __init__(self):
        self.filesDownloader = FilesDownloader()
        self.pathsProvider = PathsProvider()
        self.directoryManager = DirectoryManager()

    def get_recognition_input_file_path(self, request_id):
        self.filesDownloader.download_recognition_input(request_id)
        request_path = os.path.join(self.pathsProvider.local_recognition_image_path(), str(request_id))
        input_file_path = self.directoryManager.get_file_from_directory(request_path)
        return input_file_path

    def get_detection_input_file_path(self, request_id):
        self.filesDownloader.download_detection_input(request_id)
        request_path = os.path.join(self.pathsProvider.local_detection_image_path(), str(request_id))
        input_file_path = self.directoryManager.get_file_from_directory(request_path)
        return input_file_path
