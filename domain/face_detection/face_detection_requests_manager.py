import cv2

from configuration_global.config_reader import ConfigReader
from configuration_global.exception_handler import exception
from configuration_global.logger_factory import LoggerFactory
from dataLayer.entities.detection import Detection
from dataLayer.repositories.face_detection_repository import FaceDetectionRepository
from domain.face_detection.results_operator import ResultsOperator
from domain.face_detection.face_detectors_manager import FaceDetectorsManager
from dropbox_integration.dropbox_client import DropboxClient


class FaceDetectionRequestsManager():
    def __init__(self):
        self.config = ConfigReader()
        self.dbxClient = DropboxClient()
        self.faceDetectorsManager = FaceDetectorsManager()
        self.logger = LoggerFactory()
        self.faceDetectionRepository = FaceDetectionRepository()
        self.resultsOperator = ResultsOperator()

        self.requests_path = self.config.face_detection_requests_path
        self.dropbox_base_path = "DetectionImage"

    @exception
    def process_request(self, request: Detection):
        self.logger.info(f"Working on Face Detection Request id: {request.id} started")
        input_file = self.dbxClient.download_single_file_from_folder(f"{self.dropbox_base_path}/{request.id}",
                                                                     self.requests_path)
        image = cv2.imread(f'{self.requests_path}{self.dropbox_base_path}/{request.id}/{input_file}')
        faces_detected_by_haar, faces_detected_by_dnn = self.faceDetectorsManager.get_faces_on_image(image)
        save_path = f"{self.requests_path}{self.dropbox_base_path}/{request.id}"
        self.resultsOperator.prepare_results(save_path, faces_detected_by_dnn, faces_detected_by_haar, image)
        self.resultsOperator.upload_results(save_path, f"{self.dropbox_base_path}/{request.id}", request.id)
        self.faceDetectionRepository.complete_request(request.id, len(faces_detected_by_haar),
                                                      len(faces_detected_by_dnn))
        self.logger.info(f"Finished Face Detection Request id: {request.id} ")
