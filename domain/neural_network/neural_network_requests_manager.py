from configuration_global.exception_handler import exception
from configuration_global.logger_factory import LoggerFactory
from dataLayer.entities.neural_network import NeuralNetwork
from dataLayer.repositories.neural_network_repository import NeuralNetworkRepository
from domain.neural_network.neural_network_uploader import NeuralNetworkUploader
from domain.neural_network.training_data_provider import TrainingDataProvider
from opencv_client.neural_network.neural_network_trainer import NeuralNetworkTrainer


class NeuralNetworkRequestsManager():
    def __init__(self):
        self.logger = LoggerFactory()
        self.trainingDataProvider = TrainingDataProvider()
        self.neuralNetworkTrainer = NeuralNetworkTrainer()
        self.neuralNetworkRepo = NeuralNetworkRepository()
        self.neuralNetworkResultUploader = NeuralNetworkUploader()

    @exception
    def process_request(self, request: NeuralNetwork):
        self.logger.info(f"Working on creating neural network for request with {request.id} id")
        face_samples, ids = self.trainingDataProvider.get_training_data_for_neural_network(request.id)
        self.neuralNetworkTrainer.create_all_face_recognizers(request.id, face_samples, ids)
        self.neuralNetworkResultUploader.upload_files(request.id)
        self.neuralNetworkRepo.complete_request(request.id)
