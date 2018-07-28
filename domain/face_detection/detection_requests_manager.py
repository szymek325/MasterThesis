import os

import cv2

from configuration_global.logger_factory import LoggerFactory
from configuration_global.paths_provider import PathsProvider
from dataLayer.entities.detection import Detection
from dataLayer.repositories.face_detection_repository import FaceDetectionRepository
from dataLayer.type_providers.detection_types import DetectionTypes
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
        self.detectionTypes = DetectionTypes()

    def process_request(self, request: Detection):
        self.logger.info(f"Working on Face Detection Request id: {request.id} started")
        input_file_path = self.__get_input_filepath__(request.id)
        results = self.faceDetectorsManager.get_faces_on_image_from_file_path(input_file_path)
        self.__finish_request__(results, input_file_path, request.id)
        self.logger.info(f"Finished Face Detection Request id: {request.id} ")

    def __get_input_filepath__(self, request_id):
        self.filesDownloader.download_detection_input(request_id)
        request_path = os.path.join(self.pathsProvider.local_detection_image_path(), str(request_id))
        input_file_path = self.directoryManager.get_file_from_directory(request_path)
        return input_file_path

    def __finish_request__(self, results, input_file_path, request_id):
        self.resultsOperator.upload_results(request_id, results, input_file_path)
        self.faceDetectionRepository.complete_request(request_id)
