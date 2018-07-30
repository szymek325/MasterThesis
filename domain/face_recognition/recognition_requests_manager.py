from configuration_global.logger_factory import LoggerFactory
from dataLayer.entities.recognition import Recognition
from dataLayer.repositories.face_recognition_repository import FaceRecognitionRepository
from domain.face_recognition.face_recognizers_manager import FaceRecognizersManager
from domain.input_file_provider import InputFileProvider


class RecognitionRequestManager():
    def __init__(self):
        self.logger = LoggerFactory()
        self.inputFileProvider = InputFileProvider()
        self.recognitionRepository = FaceRecognitionRepository()
        self.recognizersManager = FaceRecognizersManager()

    def process_request(self, request: Recognition):
        self.logger.info(f"Working on face recognition request {request.id} id")
        input_file_path = self.inputFileProvider.get_recognition_input_file_path(request.id)
        self.recognizersManager.get_identity_by_open_cv_recognizers(request.id, request.neural_network_id,input_file_path)
        self.logger.info(f"Completed recognition request id: {request.id} name: {request.name}")
