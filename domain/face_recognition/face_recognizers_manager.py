from cognitive_face_client.azure_face_client import AzureFaceClient
from cognitive_face_client.azure_large_groups_client import AzureLargeGroupsClient
from configuration_global.logger_factory import LoggerFactory
from domain.face_recognition.face_recognizer_provider import FaceRecognizerProvider
from opencv_client.face_recognition.face_recognizer import FaceRecognizer


class FaceRecognizersManager():
    def __init__(self):
        self.logger = LoggerFactory()
        self.faceRecognizerProvider = FaceRecognizerProvider()
        self.faceRecognizer = FaceRecognizer()
        self.azureNnClient = AzureLargeGroupsClient()
        self.azureFaceClient = AzureFaceClient()

    def get_identity_by_open_cv_recognizers(self, request_id, neural_network_id, image_file_path):
        face_recognizers = self.faceRecognizerProvider.create_face_recognizers_for_request(neural_network_id)
        self.faceRecognizer.recognize_face_from_image(request_id, face_recognizers, image_file_path)

    def get_identity_by_azure_cognitive(self, reqeust_id, neural_network_id, image_file_path):
        # need to get dictionary of faces for identitifaction
        face_ids = self.azureFaceClient.get_face_ids(image_file_path)
        recognized_ids= self.azureFaceClient.get_faces_identity(face_ids,reqeust_id)
