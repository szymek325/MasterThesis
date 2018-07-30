import ast

from sqlalchemy import null

from cognitive_face_client.azure_face_client import AzureFaceClient
from cognitive_face_client.azure_large_groups_client import AzureLargeGroupsClient
from configuration_global.logger_factory import LoggerFactory
from dataLayer.repositories.neural_network_file_repository import NeuralNetworkFileRepository
from dataLayer.repositories.recognition_result_repository import RecognitionResultRepository
from domain.face_recognition.face_recognizer_provider import FaceRecognizerProvider
from opencv_client.face_recognition.face_recognizer import FaceRecognizer


class FaceRecognizersManager():
    def __init__(self):
        self.logger = LoggerFactory()
        self.faceRecognizerProvider = FaceRecognizerProvider()
        self.faceRecognizer = FaceRecognizer()
        self.azureNnClient = AzureLargeGroupsClient()
        self.azureFaceClient = AzureFaceClient()
        self.neuralNetworkFilesRepo = NeuralNetworkFileRepository()
        self.recognitionResultRepo = RecognitionResultRepository()

    def get_identity_by_open_cv_recognizers(self, request_id, neural_network_id, image_file_path):
        face_recognizers = self.faceRecognizerProvider.create_face_recognizers_for_request(neural_network_id)
        self.faceRecognizer.recognize_face_from_image(request_id, face_recognizers, image_file_path)

    def get_identity_by_azure_cognitive(self, request_id, neural_network_id, image_file_path):
        azure_file = self.neuralNetworkFilesRepo.get_azure_file_connected_to_neural_network(neural_network_id)
        if azure_file is not null:
            dictionary = ast.literal_eval(azure_file.additional_data)
            face_ids = self.azureFaceClient.get_face_ids(image_file_path)
            recognized_azure_ids = self.azureFaceClient.get_faces_identity(face_ids, azure_file.neuralNetworkId)
            for face_id, rec_az_id in recognized_azure_ids.items():
                person_identity = self.__get_person_id__(rec_az_id, dictionary)
                self.recognitionResultRepo.add_recognition_result(person_identity, request_id, 1, azure_file.id)

    def __get_person_id__(self, azure_id, people_dictionary):
        for person_id, azure_person_id in people_dictionary.items():
            if azure_person_id == azure_id:
                return person_id
        return 0
