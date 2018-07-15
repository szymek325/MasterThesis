import os

from configuration_global.paths_provider import PathsProvider
from dataLayer.repositories.neural_network_file_repository import NeuralNetworkFileRepository
from dataLayer.repositories.neural_network_type_repository import NeuralNetworkTypeRepository
from opencv_client.neural_network.neural_network_provider import NeuralNetworkProvider


class FaceRecognizerManager():
    def __init__(self):
        self.pathsProvider = PathsProvider()
        self.neuralNetworkProvider = NeuralNetworkProvider()
        self.neuralNetworkFilesRepo = NeuralNetworkFileRepository()
        self.nnTypesRepo = NeuralNetworkTypeRepository()

    def recognize_face_on_image(self, request_id, image):
        nn_files = self.neuralNetworkFilesRepo.get_all_files_connected_to_neural_network_with_neural_types(request_id)
        types = self.nnTypesRepo.get_all_types().all()
        face_recognizers = []
        for file in nn_files:
            nn_path = os.path.join(self.pathsProvider.local_neural_network_path(), str(file.neuralNetworkId), file.name)
            # TODO complete face recognizing
            self.neuralNetworkProvider.create_neural_network(nn_path, file.neuralNetworkType.name)
