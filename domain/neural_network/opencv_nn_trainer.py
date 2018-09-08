import time

from domain.neural_network.neural_network_results_saver import NeuralNetworkResultsSaver
from domain.neural_network.training_data_provider import TrainingDataProvider
from opencv_client.neural_network.neural_network_trainer import NeuralNetworkTrainer


class OpenCvNnTrainer():
    def __init__(self):
        self.trainingDataProvider = TrainingDataProvider()
        self.neuralNetworkTrainer = NeuralNetworkTrainer()
        self.neuralNetworkResultUploader = NeuralNetworkResultsSaver()

    def train_open_cv(self, request_id, people_with_image_paths):
        start_data = time.time()
        training_data = self.trainingDataProvider.get_training_data_for_neural_network(request_id,
                                                                                       people_with_image_paths)
        end_data = time.time()
        data_preparation_time = end_data - start_data
        training_times = self.neuralNetworkTrainer.create_all_face_recognizers(request_id, training_data)
        self.neuralNetworkResultUploader.save_result_files(request_id, training_times, data_preparation_time)
