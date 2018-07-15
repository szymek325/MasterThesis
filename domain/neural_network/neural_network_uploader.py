from os import path, listdir

from configuration_global.logger_factory import LoggerFactory
from configuration_global.paths_provider import PathsProvider
from dataLayer.repositories.neural_network_file_repository import NeuralNetworkFileRepository
from dataLayer.repositories.neural_network_type_repository import NeuralNetworkTypeRepository
from domain.string_operator import StringOperator
from dropbox_integration.files_uploader import FilesUploader


class NeuralNetworkUploader():
    def __init__(self):
        self.logger = LoggerFactory()
        self.pathsProvider = PathsProvider()
        self.filesUploader = FilesUploader()
        self.nnFilesRepo = NeuralNetworkFileRepository()
        self.nnTypesRepo = NeuralNetworkTypeRepository()
        self.stringOperator = StringOperator()

    def upload_files(self, neural_network_id):
        base_path = path.join(self.pathsProvider.local_neural_network_path(), str(neural_network_id))
        file_paths = [path.join(base_path, f) for f in listdir(base_path)]
        for file_path in file_paths:
            opened_file = open(file_path, 'rb')
            file_name, nn_type_id = self.__get_file_name_and_file_type_id(file_path)
            self.nnFilesRepo.add_neural_network_file(file_name, neural_network_id, nn_type_id)
            self.filesUploader.upload_neural_network(neural_network_id, opened_file.read(), file_name)
            self.logger.info(f"Uploaded file: {file_name}")

    def __get_file_name_and_file_type_id(self, file_path):
        file_name = self.stringOperator.get_file_name_from_path(file_path)
        nn_type_name = self.stringOperator.find_between(file_name, '_', '.')
        nn_type_id = self.nnTypesRepo.get_id_by_name(nn_type_name)
        return file_name, nn_type_id
