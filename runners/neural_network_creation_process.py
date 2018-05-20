from configuration_global.config_reader import ConfigReader
from configuration_global.exception_handler import exception
from configuration_global.logger_factory import LoggerFactory
from sqlalchemy import null

from dataLayer.repositories.neural_network_repository import NeuralNetworkRepository
from domain.neural_network_requests_manager import NeuralNetworkRequestsManager


class NeuralNetworkCreationProcess():
    def __init__(self):
        self.logger = LoggerFactory()
        self.config = ConfigReader()
        self.nnManager = NeuralNetworkRequestsManager()
        self.nnRepo = NeuralNetworkRepository()

    @exception
    def run_nn_training(self):
        requests = self.nnRepo.get_all_not_completed()
        if not requests == null:
            for request in requests:
                self.nnManager.process_request(request)
        self.logger.info("Processing done")


if __name__ == "__main__":
    drop = NeuralNetworkCreationProcess()
    drop.run_nn_training()
