import cv2

from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from dataLayer.entities.detection import Detection
from dataLayer.repositories.face_detection_repository import FaceDetectionRepository
from domain.face_detection.results_operator import ResultsOperator
from domain.face_detection.face_detectors_manager import FaceDetectorsManager
from dropbox_integration.files_downloader import FilesDownloader


class FaceDetectionRequestsManager():
    def __init__(self):
        self.config = ConfigReader()
        self.logger = LoggerFactory()
        self.faceDetectionRepository = FaceDetectionRepository()
        self.faceDetectorsManager = FaceDetectorsManager()
        self.resultsOperator = ResultsOperator()
        self.filesDownloader = FilesDownloader()

    def process_request(self, request: Detection):
        self.logger.info(f"Working on Face Detection Request id: {request.id} started")
        input_file_path = self.filesDownloader.download_detection_input(request.id)
        image = cv2.imread(input_file_path)
        faces_detected_by_haar, faces_detected_by_dnn = self.faceDetectorsManager.get_faces_on_image(image)
        self.resultsOperator.upload_results(request.id, faces_detected_by_dnn, faces_detected_by_haar, image)
        self.faceDetectionRepository.complete_request(request.id, len(faces_detected_by_haar), len(faces_detected_by_dnn))
        self.logger.info(f"Finished Face Detection Request id: {request.id} ")
