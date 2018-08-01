import ast

from cognitive_face_client.clients.azure_face_client import AzureFaceClient
from cognitive_face_client.clients.azure_large_groups_client import AzureLargeGroupsClient
from dataLayer.repositories.neural_network_file_repository import NeuralNetworkFileRepository
from dataLayer.repositories.recognition_result_repository import RecognitionResultRepository


class AzureFaceRecognizer():
    def __init__(self):
        self.azureNnClient = AzureLargeGroupsClient()
        self.azureFaceClient = AzureFaceClient()
        self.neuralNetworkFilesRepo = NeuralNetworkFileRepository()
        self.recognitionResultRepo = RecognitionResultRepository()

    #TODO add null from sql alchemy
    def recognize_face(self, request_id, neural_network_id, image_file_path):
        azure_file = self.neuralNetworkFilesRepo.get_azure_file_connected_to_neural_network(neural_network_id)
        if azure_file is not null:
            dictionary = ast.literal_eval(azure_file.additional_data)
            face_ids = self.azureFaceClient.get_face_ids(image_file_path)
            recognized_azure_ids = self.azureFaceClient.get_faces_identity(face_ids, azure_file.neuralNetworkId)
            for face_id, rec_az_id in recognized_azure_ids.items():
                person_identity = self.__get_person_id__(rec_az_id, dictionary)
                self.recognitionResultRepo.add_recognition_result(person_identity[0], request_id, person_identity[2],
                                                                  azure_file.id,
                                                                  person_identity[1])

    def __get_person_id__(self, azure_id, people_dictionary):
        for person_id, azure_person_id in people_dictionary.items():
            if azure_person_id == azure_id:
                return person_id, "", 1
        if azure_id == 'Unknown':
            return 0, "Face found. Person is Unknown", 0
