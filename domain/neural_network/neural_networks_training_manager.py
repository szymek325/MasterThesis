from configuration_global.logger_factory import LoggerFactory
from dataLayer.repositories.neural_network_person_repository import NeuralNetworkPersonRepository
from dataLayer.repositories.neural_network_repository import NeuralNetworkRepository
from domain.neural_network.azure_large_group_trainer import AzureLargeGroupTrainer
from domain.neural_network.opencv_nn_trainer import OpenCvNnTrainer
from domain.people.people_images_provider import PeopleImagesProvider


class NeuralNetworksTrainingManager():
    def __init__(self):
        self.logger = LoggerFactory()
        self.neuralNetworkPersonRepo = NeuralNetworkPersonRepository()
        self.peopleImagesProvider = PeopleImagesProvider()
        self.neuralNetworkRepo = NeuralNetworkRepository()
        self.azureGroupTrainer = AzureLargeGroupTrainer()
        self.openCvNnTrainer = OpenCvNnTrainer()

    def create_all_neural_networks(self, request_id, request_name, max_photos_per_person):
        people_ids = self.neuralNetworkPersonRepo.get_all_people_connected_to_neural_network(request_id)
        people_with_image_paths = self.peopleImagesProvider.get_image_paths_for_people(people_ids,
                                                                                       max_photos_per_person)
        self.openCvNnTrainer.train_open_cv(request_id, people_with_image_paths)
        self.azureGroupTrainer.train_large_group(request_id, request_name, people_with_image_paths)
