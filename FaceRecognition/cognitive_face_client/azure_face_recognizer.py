import time

from sqlalchemy import null

from cognitive_face_client.clients.azure_face_client import AzureFaceClient
from cognitive_face_client.clients.azure_large_groups_client import AzureLargeGroupsClient
from dataLayer.entities.recognition_result import RecognitionResult
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

    def recognize_face_without_db(self, neural_network_id, image_file_path):
        azure_file = self.neuralNetworkFilesRepo.get_azure_file_connected_to_neural_network(neural_network_id)
        if azure_file is not null and azure_file is not None:
            return self.__get_result_from_azure_without_db__(azure_file, image_file_path)

    def __get_result_from_azure_without_db__(self, azure_file, image_file_path):
        start_time = time.time()
        face_ids = self.azureFaceClient.get_face_ids(image_file_path)
        if len(face_ids) is 0:
            return 0
        recognized_azure_ids = self.azureFaceClient.get_faces_identity(face_ids, azure_file.neuralNetworkId)
        if len(recognized_azure_ids) is 0:
            return 0
        for face_id, rec_az_id in recognized_azure_ids.items():
            person_identity = self.azureNnClient.get_person_in_large_group_name(azure_file.neuralNetworkId,
                                                                                rec_az_id['personId'])
            return person_identity

    def __get_result_from_azure__(self, azure_file, image_file_path, request_id):
        start_time = time.time()
        face_ids = self.azureFaceClient.get_face_ids(image_file_path)
        if len(face_ids) is 0:
            self.__add_empty_result__(azure_file, request_id, start_time, "No faces found")
            return
        recognized_azure_ids = self.azureFaceClient.get_faces_identity(face_ids, azure_file.neuralNetworkId)
        if len(recognized_azure_ids) is 0:
            self.__add_empty_result__(azure_file, request_id, start_time, "Person unknown")
            return
        for face_id, rec_az_id in recognized_azure_ids.items():
            self.__add_results_for_recognized_faces__(azure_file, rec_az_id, request_id, start_time)

    def __add_empty_result__(self, azure_file, request_id, start_time, message):
        end_time = time.time()
        process_time = end_time - start_time
        result = RecognitionResult(0, request_id, 0, azure_file.id, str(process_time), message)
        self.recognitionResultRepo.add_recognition_result(result)

    def __add_results_for_recognized_faces__(self, azure_file, rec_az_id, request_id, start_time):
        end_time = time.time()
        process_time = end_time - start_time
        person_identity = self.azureNnClient.get_person_in_large_group_name(azure_file.neuralNetworkId,
                                                                            rec_az_id['personId'])
        result = RecognitionResult(int(person_identity), request_id, rec_az_id['confidence'], azure_file.id,
                                   str(process_time), "")
        self.recognitionResultRepo.add_recognition_result(result)
