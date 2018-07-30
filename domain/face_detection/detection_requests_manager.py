from configuration_global.logger_factory import LoggerFactory
from dataLayer.entities.detection import Detection
from domain.face_detection.results_operator import ResultsOperator
from domain.face_detection.face_detectors_manager import FaceDetectorsManager
from domain.input_file_provider import InputFileProvider


class DetectionRequestsManager():
    def __init__(self):
        self.logger = LoggerFactory()
        self.faceDetectorsManager = FaceDetectorsManager()
        self.resultsOperator = ResultsOperator()
        self.inputFileProvider = InputFileProvider()

    def process_request(self, request: Detection):
        self.logger.info(f"Working on Face Detection Request id: {request.id} started")
        input_file_path = self.inputFileProvider.get_detection_input_file_path(request.id)
        self.logger.info(f"Running all face detectors")
        results = self.faceDetectorsManager.get_faces_on_image_from_file_path(input_file_path)
        self.resultsOperator.prepare_and_upload_results(request.id, results, input_file_path)
        self.logger.info(f"Finished Face Detection Request id: {request.id} ")
