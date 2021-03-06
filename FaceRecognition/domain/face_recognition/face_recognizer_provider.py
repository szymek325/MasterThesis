import os

from configuration_global.paths_provider import PathsProvider
from dataLayer.repositories.neural_network_file_repository import NeuralNetworkFileRepository
from dataLayer.type_providers.neural_network_types import NeuralNetworkTypes
from opencv_client.neural_network.neural_network_provider import NeuralNetworkProvider


class FaceRecognizerProvider():
    def __init__(self):
        self.pathsProvider = PathsProvider()
        self.neuralNetworkProvider = NeuralNetworkProvider()
        self.neuralNetworkFilesRepo = NeuralNetworkFileRepository()

    def create_open_cv_face_recognizers_for_request(self, request_id):
        nn_files = self.neuralNetworkFilesRepo.get_all_open_cv_files_connected_to_neural_network(request_id)
        face_recognizers = []
        for file in nn_files:
            nn_path = os.path.join(self.pathsProvider.local_neural_network_path(), str(file.neuralNetworkId), file.name)
            recognizer = self.neuralNetworkProvider.create_neural_network_by_type_id(nn_path, file.neuralNetworkTypeId)
            face_recognizers.append([recognizer, file.id])
        return face_recognizers

    def create_open_cv_face_recognizers_with_type(self, request_id):
        nn_files = self.neuralNetworkFilesRepo.get_all_open_cv_files_connected_to_neural_network(request_id)
        eigen = []
        fisher = []
        lbph = []
        for file in nn_files:
            nn_path = os.path.join(self.pathsProvider.local_neural_network_path(), str(file.neuralNetworkId), file.name)
            recognizer = self.neuralNetworkProvider.create_neural_network_by_type_id(nn_path, file.neuralNetworkTypeId)
            if file.neuralNetworkTypeId is NeuralNetworkTypes().fisher_id:
                fisher = recognizer
            elif file.neuralNetworkTypeId is NeuralNetworkTypes().eigen_id:
                eigen = recognizer
            elif file.neuralNetworkTypeId is NeuralNetworkTypes().lbph_id:
                lbph = recognizer
        return fisher, eigen, lbph
