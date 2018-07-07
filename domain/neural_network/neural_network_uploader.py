from os import path, listdir

from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from dropbox_integration.dropbox_client import DropboxClient


class NeuralNetworkUploader():
    def __init__(self):
        self.dropboxClient = DropboxClient()
        self.config = ConfigReader()
        self.logger = LoggerFactory()
        self.nn_path = self.config.neural_networks_path

    def upload_files(self, neural_network_id):
        base_path = path.join(self.nn_path, f'{neural_network_id}')
        files = [path.join(base_path, f) for f in listdir(base_path)]
        for file in files:
            opened_file = open(f"{file}", 'rb')
            file_name = file.split('\\')[-1]
            self.logger.info(file_name)
            self.dropboxClient.upload_file(opened_file.read(), f"neuralNetworks/{neural_network_id}", file_name)
