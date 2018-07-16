from configuration_global.exception_handler import exception
from configuration_global.logger_factory import LoggerFactory
from configuration_global.paths_provider import PathsProvider
from dataLayer.repositories.neural_network_repository import NeuralNetworkRepository
from domain.directory_manager import DirectoryManager
from dropbox_integration.files_downloader import FilesDownloader


class NeuralNetworksManager():
    def __init__(self):
        self.pathProvider = PathsProvider()
        self.logger = LoggerFactory()
        self.filesDownloader = FilesDownloader()
        self.nnRepo = NeuralNetworkRepository()
        self.directoryManager = DirectoryManager()

    @exception
    def download_neural_networks_to_local(self):
        nn_path = self.pathProvider.local_neural_network_path()
        directories = self.directoryManager.get_subdirectories_with_files_count(nn_path)
        neural_networks = self.nnRepo.get_neural_networks_ids_with_files_count()
        nns_to_download = [x for x in neural_networks if x not in directories]
        self.logger.info(f"directories {directories} "
                         f"\nneural_networks: {neural_networks}"
                         f"\nneural_networks_to_download: {nns_to_download}")
        for nn in nns_to_download:
            self.filesDownloader.download_neural_network(nn[0])
