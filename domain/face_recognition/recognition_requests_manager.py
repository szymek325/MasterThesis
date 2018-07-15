import cv2

from configuration_global.logger_factory import LoggerFactory
from dataLayer.entities.recognition import Recognition
from domain.face_recognition.face_recognizer_manager import FaceRecognizerManager
from dropbox_integration.files_downloader import FilesDownloader
from opencv_client.face_recognition.face_recognizer import FaceRecognizer


class RecognitionRequestManager():
    def __init__(self):
        self.logger = LoggerFactory()
        self.filesDownloader = FilesDownloader()
        self.faceRecognizerManager = FaceRecognizerManager()

    def process_request(self, request: Recognition):
        self.logger.info(f"Working on face recognition request {request.id} id")
        input_file_path = self.filesDownloader.download_recognition_input(request.id)
        image = cv2.imread(input_file_path)
        self.faceRecognizerManager.recognize_face_on_image(request.id, image)
        # TODO recognize face using different neural networks here!
        # try:
        #     input_file = self.dbxClient.download_single_file(request.guid, self.requests_path)
        #     image = cv2.imread(f'{self.requests_path}{request.guid}/{input_file}')
        #     result = self.faceRecognizer.recognize_face_on_image(image, request.neural_network_id)
        #     self.logger.info(f"result : {result}")
        # except:
        #     self.logger.error(f"Exception occurred during face detection request {request.id} ")
        # self.logger.info(f"Finished Face Detection Request id: {request.id} ")
