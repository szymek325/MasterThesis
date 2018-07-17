from configuration_global.exception_handler import exception
from configuration_global.logger_factory import LoggerFactory
from dataLayer.entities.recognition import Recognition
from dataLayer.repositories.face_recognition_repository import FaceRecognitionRepository
from domain.face_recognition.face_recognizer_provider import FaceRecognizerProvider
from dropbox_integration.files_downloader import FilesDownloader
from opencv_client.face_recognition.face_recognizer import FaceRecognizer


class RecognitionRequestManager():
    def __init__(self):
        self.logger = LoggerFactory()
        self.filesDownloader = FilesDownloader()
        self.faceRecognizerProvider = FaceRecognizerProvider()
        self.faceRecognizer = FaceRecognizer()
        self.recognitionRepository = FaceRecognitionRepository()

    @exception
    def process_request(self, request: Recognition):
        self.logger.info(f"Working on face recognition request {request.id} id")
        input_file_path = self.filesDownloader.download_recognition_input(request.id)
        face_recognizers = self.faceRecognizerProvider.create_face_recognizers_for_request(request.neural_network_id)
        self.faceRecognizer.recognize_face_from_image(request.id, face_recognizers, input_file_path)
        self.recognitionRepository.complete_request(request.id)
        self.logger.info(f"Completed recognition request id: {request.id} name: {request.name}")