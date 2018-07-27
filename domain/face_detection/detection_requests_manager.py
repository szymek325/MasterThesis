import os

import cv2

from configuration_global.logger_factory import LoggerFactory
from configuration_global.paths_provider import PathsProvider
from dataLayer.entities.detection import Detection
from dataLayer.repositories.face_detection_repository import FaceDetectionRepository
from domain.directory_manager import DirectoryManager
from domain.face_detection.results_operator import ResultsOperator
from domain.face_detection.face_detectors_manager import FaceDetectorsManager
from dropbox_integration.files_downloader import FilesDownloader


class DetectionRequestsManager():
    def __init__(self):
        self.logger = LoggerFactory()
        self.faceDetectionRepository = FaceDetectionRepository()
        self.faceDetectorsManager = FaceDetectorsManager()
        self.resultsOperator = ResultsOperator()
        self.filesDownloader = FilesDownloader()
        self.pathsProvider = PathsProvider()
        self.directoryManager = DirectoryManager()

    def process_request(self, request: Detection):
        self.logger.info(f"Working on Face Detection Request id: {request.id} started")
        input_file_path = self.__get_input_filepath__(request.id)
        image = cv2.imread(input_file_path)
        faces_detected_by_haar, faces_detected_by_dnn = self.faceDetectorsManager.get_faces_on_image(image)
        faces_detected_by_azure = self.faceDetectorsManager.get_face_by_azure(input_file_path)
        self.__finish_request__(faces_detected_by_dnn, faces_detected_by_haar, image, request.id)
        self.logger.info(f"Finished Face Detection Request id: {request.id} ")

    def __get_input_filepath__(self, request_id):
        self.filesDownloader.download_detection_input(request_id)
        request_path = os.path.join(self.pathsProvider.local_detection_image_path(), str(request_id))
        input_file_path = self.directoryManager.get_file_from_directory(request_path)
        return input_file_path

    def __finish_request__(self, faces_detected_by_dnn, faces_detected_by_haar, image, request_id):
        self.resultsOperator.upload_results(request_id, faces_detected_by_dnn, faces_detected_by_haar, image)
        self.faceDetectionRepository.complete_request(request_id, len(faces_detected_by_haar),
                                                      len(faces_detected_by_dnn))
