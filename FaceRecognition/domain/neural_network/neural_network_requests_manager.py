from configuration_global.logger_factory import LoggerFactory
from dataLayer.entities.neural_network import NeuralNetwork
from dataLayer.repositories.neural_network_repository import NeuralNetworkRepository
from domain.neural_network.neural_networks_training_manager import NeuralNetworksTrainingManager


class NeuralNetworkRequestsManager():
    def __init__(self):
        self.logger = LoggerFactory()
        self.neuralNetworkRepo = NeuralNetworkRepository()
        self.neuralNetworksTrainingManager = NeuralNetworksTrainingManager()

    def process_request(self, request: NeuralNetwork):
        self.logger.info(f"Working on creating neural network for request with {request.id} id")
        self.neuralNetworksTrainingManager.create_all_neural_networks(request.id, request.name,
                                                                      request.maxNumberOfPhotosPerPerson)
        self.neuralNetworkRepo.complete_request(request.id)
