import os

from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from dataLayer.entities.neural_network import NeuralNetwork
from neuralNetwork.neural_network_trainer import NeuralNetworkTrainer
from neuralNetwork.training_data_converter import TrainingDataConverter
from people.people_downloader import PeopleDownloader


class NeuralNetworkRequestsManager():
    def __init__(self):
        self.config = ConfigReader()
        self.logger = LoggerFactory()
        self.peopleDownloader = PeopleDownloader()
        self.trainingDataConverter = TrainingDataConverter()
        self.neuralNetworkTrainer = NeuralNetworkTrainer()

    def process_request(self, request: NeuralNetwork):
        people_ids = self.peopleDownloader.get_all_required_people_to_local(request.id)
        face_samples, ids = self.trainingDataConverter.get_training_data(people_ids)
        self.logger.info(face_samples)
        self.logger.info(ids)
        self.neuralNetworkTrainer.create_all_face_recognizers(request.id, face_samples, ids)
