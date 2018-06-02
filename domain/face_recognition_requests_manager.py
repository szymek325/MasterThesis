import cv2

from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from dataLayer.entities.face_recognition import FaceRecognition
from dropbox_integration.dropbox_client import DropboxClient
from faceRecognition.face_recognition_worker import FaceRecognitionWorker


class FaceRecognitionRequestManager():
    def __init__(self):
        self.logger = LoggerFactory()
        self.config = ConfigReader()
        self.faceRecognizer = FaceRecognitionWorker()
        self.dbxClient = DropboxClient()
        self.requests_path = self.config.face_recognition_requests_path

    def process_request(self, request: FaceRecognition):
        self.logger.info(f"Working on face recognition request {request.id} id")
        try:
            input_file = self.dbxClient.download_single_file_from_folder(request.guid, self.requests_path)
            image = cv2.imread(f'{self.requests_path}{request.guid}/{input_file}')
            result = self.faceRecognizer.recognize_face(image, request.neural_network_id)
            self.logger.info(f"result : {result}")
        except:
            self.logger.error(f"Exception occurred during face detection request {request.id} ")
        self.logger.info(f"Finished Face Detection Request id: {request.id} ")
