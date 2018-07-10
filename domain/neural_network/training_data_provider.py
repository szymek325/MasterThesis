import os

from configuration_global.logger_factory import LoggerFactory
from configuration_global.paths_provider import PathsProvider
from dataLayer.repositories.neural_network_person_repository import NeuralNetworkPersonRepository
import numpy as np

from opencv_client.face_recognition.training_input_preparator import TrainingDataExtractor


class TrainingDataProvider():
    def __init__(self):
        self.logger = LoggerFactory()
        self.pathsProvider = PathsProvider()
        self.neuralNetworkPersonRepo = NeuralNetworkPersonRepository()
        self.trainingDataExtractor = TrainingDataExtractor()

    def get_training_data_for_neural_network(self, request_id: int):
        self.logger.info(f"Preparing training data for NeuralNetwork request : {request_id}")
        people_ids = self.neuralNetworkPersonRepo.get_all_people_connected_to_neural_network(request_id)
        face_samples = []
        ids = []
        for person_id in people_ids:
            person_path = os.path.join(self.pathsProvider.local_person_image_path(), str(person_id))
            image_paths = [os.path.join(person_path, f) for f in os.listdir(person_path)]
            extracted_faces, faces_ids = self.trainingDataExtractor.extract_training_data_from_images(image_paths,
                                                                                                      person_id)
            face_samples.append(extracted_faces)
            ids.append(faces_ids)
        self.logger.info(f"face_samples:\n {face_samples}"
                         f"\nids: {ids}")
        self.logger.info(f"Preparing training data for NeuralNetwork request : {request_id} FINISHED")
        return face_samples, np.array(ids)
