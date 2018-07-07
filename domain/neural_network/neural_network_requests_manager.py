from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from dataLayer.entities.neural_network import NeuralNetwork
from dataLayer.repositories.neural_network_repository import NeuralNetworkRepository
from domain.neural_network.neural_network_uploader import NeuralNetworkUploader
from opencv_client.face_recognition.neural_network_trainer import NeuralNetworkTrainer
from opencv_client.face_recognition.training_data_converter import TrainingDataConverter
from domain.people.people_downloader import PeopleDownloader


class NeuralNetworkRequestsManager():
    def __init__(self):
        self.config = ConfigReader()
        self.logger = LoggerFactory()
        self.peopleDownloader = PeopleDownloader()
        self.trainingDataConverter = TrainingDataConverter()
        self.neuralNetworkTrainer = NeuralNetworkTrainer()
        self.neuralNetworkRepo = NeuralNetworkRepository()
        self.neuralNetworkUploader = NeuralNetworkUploader()

    def process_request(self, request: NeuralNetwork):
        self.logger.info(f"Working on creating neural network for request with {request.id} id")
        people_ids = self.peopleDownloader.get_all_required_people_to_local(request.id)
        face_samples, ids = self.trainingDataConverter.get_training_data(people_ids)
        self.logger.info(face_samples)
        self.logger.info(ids)
        self.neuralNetworkTrainer.create_all_face_recognizers(request.id, face_samples, ids)
        self.neuralNetworkUploader.upload_files(request.id)
        self.neuralNetworkRepo.complete_request(request.id)
