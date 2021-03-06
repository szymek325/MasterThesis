import os

from configuration_global.logger_factory import LoggerFactory
from configuration_global.paths_provider import PathsProvider
from domain.directory_manager import DirectoryManager
from dropbox_integration.dropbox_client import DropboxClient


class FilesDownloader():
    def __init__(self):
        self.pathsProvider = PathsProvider()
        self.dropbox = DropboxClient()
        self.directory = DirectoryManager()
        self.logger = LoggerFactory()

    def download_detection_input(self, request_id: int):
        dropbox_request_path = os.path.join(self.pathsProvider.dropbox_detection_image_path(), str(request_id)).replace("\\", "/")
        local_save_path = os.path.join(self.pathsProvider.local_detection_image_path(), str(request_id))
        self.__single_file_download__(dropbox_request_path, local_save_path)
        self.logger.info(F"Finished downloading input for detection with id :{request_id}")

    def download_person(self, person_id: int):
        dropbox_person_path = os.path.join(self.pathsProvider.dropbox_person_image_path(), str(person_id)).replace("\\", "/")
        local_save_path = os.path.join(self.pathsProvider.local_person_image_path(), str(person_id))
        self.__multiple_file_download__(dropbox_person_path, local_save_path)
        self.logger.info(F"Finished downloading person with id :{person_id}")

    def download_recognition_input(self, request_id: int):
        dropbox_request_path = os.path.join(self.pathsProvider.dropbox_recognition_image_path(), str(request_id)).replace("\\", "/")
        local_save_path = os.path.join(self.pathsProvider.local_recognition_image_path(), str(request_id))
        self.__single_file_download__(dropbox_request_path, local_save_path)
        self.logger.info(F"Finished downloading input for recognition with id :{request_id}")

    def download_neural_network(self, nn_id: int):
        dropbox_neural_network_path = os.path.join(self.pathsProvider.dropbox_neural_network_path(), str(nn_id)).replace("\\", "/")
        local_save_path = os.path.join(self.pathsProvider.local_neural_network_path(), str(nn_id))
        self.__multiple_file_download__(dropbox_neural_network_path, local_save_path)
        self.logger.info(F"Finished downloading neural network with id :{nn_id}")

    def __single_file_download__(self, dropbox_request_path, local_save_path):
        self.directory.create_directory_if_doesnt_exist(local_save_path)
        self.dropbox.download_single_file(dropbox_request_path, local_save_path)

    def __multiple_file_download__(self, dropbox_folder_path, local_save_path):
        self.directory.clean_directory(local_save_path)
        self.directory.create_directory_if_doesnt_exist(local_save_path)
        self.dropbox.download_folder(dropbox_folder_path, local_save_path)
