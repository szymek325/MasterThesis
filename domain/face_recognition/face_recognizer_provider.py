import os

from configuration_global.paths_provider import PathsProvider
from dataLayer.repositories.neural_network_file_repository import NeuralNetworkFileRepository
from opencv_client.neural_network.neural_network_provider import NeuralNetworkProvider


class FaceRecognizerProvider():
    def __init__(self):
        self.pathsProvider = PathsProvider()
        self.neuralNetworkProvider = NeuralNetworkProvider()
        self.neuralNetworkFilesRepo = NeuralNetworkFileRepository()

    def create_face_recognizers_for_request(self, request_id):
        nn_files = self.neuralNetworkFilesRepo.get_all_files_connected_to_neural_network(request_id)
        face_recognizers = []
        for file in nn_files:
            nn_path = os.path.join(self.pathsProvider.local_neural_network_path(), str(file.neuralNetworkId), file.name)
            recognizer = self.neuralNetworkProvider.create_neural_network_by_type_id(nn_path, file.neuralNetworkTypeId)
            face_recognizers.append([recognizer, file.id])
        return face_recognizers
