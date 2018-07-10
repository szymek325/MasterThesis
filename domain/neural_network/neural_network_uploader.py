from os import path, listdir

from configuration_global.logger_factory import LoggerFactory
from configuration_global.paths_provider import PathsProvider
from dropbox_integration.files_uploader import FilesUploader


class NeuralNetworkUploader():
    def __init__(self):
        self.logger = LoggerFactory()
        self.pathsProvider = PathsProvider()
        self.filesUploader = FilesUploader()

    def upload_files(self, neural_network_id):
        base_path = path.join(self.pathsProvider.local_neural_network_path(), str(neural_network_id))
        files = [path.join(base_path, f) for f in listdir(base_path)]
        for file in files:
            opened_file = open(file, 'rb')
            file_name = file.split('\\')[-1]
            self.filesUploader.upload_neural_network(neural_network_id, opened_file.read(), file_name)
            self.logger.info(f"Uploaded file: {file_name}")
