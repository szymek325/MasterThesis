from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from dataLayer.entities.neural_network import NeuralNetwork
from dataLayer.repositories.neural_network_repository import NeuralNetworkRepository
from domain.neural_network.neural_network_uploader import NeuralNetworkUploader
from domain.people.people_downloader import PeopleDownloader
from opencv_client.neural_network.neural_network_trainer import NeuralNetworkTrainer
from opencv_client.face_recognition.training_data_provider import TrainingDataProvider


class NeuralNetworkRequestsManager():
    def __init__(self):
        self.config = ConfigReader()
        self.logger = LoggerFactory()
        self.peopleDownloader = PeopleDownloader()
        self.trainingDataProvider = TrainingDataProvider()
        self.neuralNetworkTrainer = NeuralNetworkTrainer()
        self.neuralNetworkRepo = NeuralNetworkRepository()
        self.neuralNetworkUploader = NeuralNetworkUploader()

    def process_request(self, request: NeuralNetwork):
        self.logger.info(f"Working on creating neural network for request with {request.id} id")
        face_samples, ids = self.trainingDataProvider.get_training_data_for_neural_network(request.id)
        self.neuralNetworkTrainer.create_all_face_recognizers(request.id, face_samples, ids)
        self.neuralNetworkUploader.upload_files(request.id)
        self.neuralNetworkRepo.complete_request(request.id)
