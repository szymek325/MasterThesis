from os import path, listdir

from configuration_global.logger_factory import LoggerFactory
from configuration_global.paths_provider import PathsProvider
from dataLayer.repositories.neural_network_file_repository import NeuralNetworkFileRepository
from domain.neural_network.file_types_provider import FileTypesProvider
from dropbox_integration.files_uploader import FilesUploader


class NeuralNetworkUploader():
    def __init__(self):
        self.logger = LoggerFactory()
        self.pathsProvider = PathsProvider()
        self.filesUploader = FilesUploader()
        self.nnFilesRepo = NeuralNetworkFileRepository()
        self.typesProvider = FileTypesProvider()

    def upload_files(self, neural_network_id):
        base_path = path.join(self.pathsProvider.local_neural_network_path(), str(neural_network_id))
        file_paths = [path.join(base_path, f) for f in listdir(base_path)]
        for file in file_paths:
            opened_file = open(file, 'rb')
            file_name = file.split('\\')[-1]
            nn_type = self.typesProvider.get_file_type_id(file_name)
            self.nnFilesRepo.add_neural_network_file(file_name, neural_network_id, nn_type)
            self.filesUploader.upload_neural_network(neural_network_id, opened_file.read(), file_name)
            self.logger.info(f"Uploaded file: {file_name}")
