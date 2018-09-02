import ast

from sqlalchemy import null

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

    def recognize_face(self, request_id, neural_network_id, image_file_path):
        azure_file = self.neuralNetworkFilesRepo.get_azure_file_connected_to_neural_network(neural_network_id)
        if azure_file is not null and azure_file is not None:
            self.__get_result_from_azure__(azure_file, image_file_path, request_id)

    def __get_result_from_azure__(self, azure_file, image_file_path, request_id):
        dictionary = ast.literal_eval(azure_file.additional_data)
        face_ids = self.azureFaceClient.get_face_ids(image_file_path)
        if len(face_ids) is 0:
            self.recognitionResultRepo.add_recognition_result(0, request_id, 0, azure_file.id, "No faces found")
            return
        recognized_azure_ids = self.azureFaceClient.get_faces_identity(face_ids, azure_file.neuralNetworkId)
        if len(recognized_azure_ids) is 0:
            self.recognitionResultRepo.add_recognition_result(0, request_id, 0, azure_file.id, "No faces found")
            return
        for face_id, rec_az_id in recognized_azure_ids.items():
            self.__add_results_for_recognized_faces__(azure_file, dictionary, rec_az_id, request_id)

    def __add_results_for_recognized_faces__(self, azure_file, dictionary, rec_az_id, request_id):
        person_identity = self.__get_person_id_from_dictionary__(rec_az_id, dictionary)
        self.recognitionResultRepo.add_recognition_result(person_identity[0], request_id, person_identity[2],
                                                          azure_file.id, person_identity[1])

    def __get_person_id_from_dictionary__(self, azure_id, people_dictionary):
        for person_id, azure_person_id in people_dictionary.items():
            if azure_person_id == azure_id['personId']:
                return person_id, "", azure_id['confidence']
        if azure_id == 'Unknown':
            return 0, "Face found. Person is Unknown", 0
