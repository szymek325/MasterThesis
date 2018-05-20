from configuration_global.config_reader import ConfigReader
from configuration_global.exception_handler import exception
from configuration_global.logger_factory import LoggerFactory
from dataLayer.repositories.neural_network_person_repository import NeuralnetworkPersonRepository
from dataLayer.repositories.neural_network_repository import NeuralNetworkRepository
from domain.directory_manager import DirectoryManager
from sqlalchemy import null


class NeuralNetworkCreationProcess():
    def __init__(self):
        self.logger = LoggerFactory()
        self.config = ConfigReader()
        self.directory = DirectoryManager()
        self.neuralNetworkRepository = NeuralNetworkRepository()
        self.nnpRepo= NeuralnetworkPersonRepository()

    @exception
    def run_nn_training(self):
        requests = self.neuralNetworkRepository.get_all_not_completed()
        if not requests == null:
            for request in requests:
                self.logger.info(f"nn Id :{request.id}")
                connected=self.nnpRepo.get_all_connected_to_request(request.id)
                for conn in connected:
                    self.logger.info(f"many to many Id :{conn.neural_network_id}")
            self.logger.info("Processing done")


if __name__ == "__main__":
    drop = NeuralNetworkCreationProcess()
    drop.run_nn_training()
