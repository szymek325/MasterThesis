from configuration_global.logger_factory import LoggerFactory
from dataLayer.repositories.neural_network_person_repository import NeuralNetworkPersonRepository
from dataLayer.repositories.neural_network_repository import NeuralNetworkRepository
from domain.neural_network.azure_large_group_trainer import AzureLargeGroupTrainer
from domain.neural_network.neural_network_uploader import NeuralNetworkUploader
from domain.neural_network.training_data_provider import TrainingDataProvider
from domain.people.people_images_provider import PeopleImagesProvider
from opencv_client.neural_network.neural_network_trainer import NeuralNetworkTrainer


class NeuralNetworksTrainingManager():
    def __init__(self):
        self.logger = LoggerFactory()
        self.neuralNetworkPersonRepo = NeuralNetworkPersonRepository()
        self.peopleImagesProvider = PeopleImagesProvider()
        self.trainingDataProvider = TrainingDataProvider()
        self.neuralNetworkTrainer = NeuralNetworkTrainer()
        self.neuralNetworkRepo = NeuralNetworkRepository()
        self.neuralNetworkResultUploader = NeuralNetworkUploader()
        self.azureGroupTrainer = AzureLargeGroupTrainer()

    def create_all_neural_networks(self, request_id, request_name):
        people_ids = self.neuralNetworkPersonRepo.get_all_people_connected_to_neural_network(request_id)
        people_with_image_paths = self.peopleImagesProvider.get_image_paths_for_people(people_ids)
        self.__create_open_cv_recognizer_neural_networks__(request_id, people_with_image_paths)
        self.azureGroupTrainer.train_large_group(request_id, request_name, people_with_image_paths)

    def __create_open_cv_recognizer_neural_networks__(self, request_id, people_with_image_paths):
        training_data = self.trainingDataProvider.get_training_data_for_neural_network(request_id, people_with_image_paths)
        self.neuralNetworkTrainer.create_all_face_recognizers(request_id, training_data)
        self.neuralNetworkResultUploader.upload_files(request_id)
