from os import path, listdir

from configuration_global.logger_factory import LoggerFactory
from configuration_global.paths_provider import PathsProvider
from dataLayer.repositories.neural_network_file_repository import NeuralNetworkFileRepository
from dataLayer.type_providers.neural_network_types import NeuralNetworkTypes
from domain.string_operator import StringOperator
from dropbox_integration.files_uploader import FilesUploader


class NeuralNetworkUploader():
    def __init__(self):
        self.logger = LoggerFactory()
        self.pathsProvider = PathsProvider()
        self.filesUploader = FilesUploader()
        self.nnFilesRepo = NeuralNetworkFileRepository()
        self.nnTypes = NeuralNetworkTypes()
        self.stringOperator = StringOperator()

    def upload_files(self, neural_network_id, training_times):
        base_path = path.join(self.pathsProvider.local_neural_network_path(), str(neural_network_id))
        file_paths = [path.join(base_path, f) for f in listdir(base_path)]
        for file_path in file_paths:
            opened_file = open(file_path, 'rb')
            file_name, nn_type_id = self.__get_file_name_and_file_type_id(file_path)
            self.__upload_single_file__(file_name, neural_network_id, nn_type_id, opened_file,
                                        training_times[nn_type_id])

    def __upload_single_file__(self, file_name, neural_network_id, nn_type_id, opened_file, process_time):
        self.logger.info(
            f"Upload of file {file_name} STARTED (possible timeout error on weak network and big file size)")
        # self.filesUploader.upload_neural_network(neural_network_id, opened_file.read(), file_name)
        self.nnFilesRepo.add_neural_network_file(file_name, neural_network_id, nn_type_id, process_time)
        self.logger.info(f"Upload of file {file_name} FINISHED")

    def __get_file_name_and_file_type_id(self, file_path):
        file_name = self.stringOperator.get_file_name_from_path(file_path)
        nn_type_name = self.stringOperator.find_between(file_name, '_', '.')
        nn_type_id = self.nnTypes.get_type_id(nn_type_name)
        return file_name, nn_type_id
