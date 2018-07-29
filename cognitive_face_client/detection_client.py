from cognitive_face_client.cognitive_client import CognitiveClient
from cognitive_face_client.results_converter import ResultsConverter
from configuration_global.logger_factory import LoggerFactory


class DetectionClient():
    def __init__(self):
        self.cognitiveClient = CognitiveClient()
        self.resultsConverter = ResultsConverter()
        self.logger = LoggerFactory()
