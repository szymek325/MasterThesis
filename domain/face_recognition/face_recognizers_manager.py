import ast

from sqlalchemy import null

from cognitive_face_client.azure_face_recognizer import AzureFaceRecognizer
from cognitive_face_client.clients.azure_face_client import AzureFaceClient
from cognitive_face_client.clients.azure_large_groups_client import AzureLargeGroupsClient
from configuration_global.logger_factory import LoggerFactory
from dataLayer.repositories.neural_network_file_repository import NeuralNetworkFileRepository
from dataLayer.repositories.recognition_result_repository import RecognitionResultRepository
from domain.face_recognition.face_recognizer_provider import FaceRecognizerProvider
from opencv_client.face_recognition.open_cv_face_recognizer import OpenCvFaceRecognizer


class FaceRecognizersManager():
    def __init__(self):
        self.logger = LoggerFactory()
        self.faceRecognizerProvider = FaceRecognizerProvider()
        self.openCvFaceRecognizer = OpenCvFaceRecognizer()
        self.azureFaceRecognizer = AzureFaceRecognizer()
        self.azureNnClient = AzureLargeGroupsClient()
        self.azureFaceClient = AzureFaceClient()
        self.neuralNetworkFilesRepo = NeuralNetworkFileRepository()
        self.recognitionResultRepo = RecognitionResultRepository()

    def get_identity_by_open_cv_recognizers(self, request_id, neural_network_id, image_file_path):
        face_recognizers = self.faceRecognizerProvider.create_open_cv_face_recognizers_for_request(neural_network_id)
        self.openCvFaceRecognizer.recognize_face_from_image(request_id, face_recognizers, image_file_path)

    def get_identity_by_azure_cognitive(self, request_id, neural_network_id, image_file_path):
        self.azureFaceRecognizer.recognize_face(request_id, neural_network_id, image_file_path)
