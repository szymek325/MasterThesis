import cognitive_face as CF

from cognitive_face_client.results_converter import ResultsConverter
from configuration_global.logger_factory import LoggerFactory


class CognitiveClient():
    def __init__(self):
        self.client = CF
        self.client.Key.set("b204cc1207534676b3d77f668405dd95")
        # klucz 2:9a8c664f40b346b7ab1bd3949a3e5f7e
        self.client.BaseUrl.set("https://westcentralus.api.cognitive.microsoft.com/face/v1.0")
        self.logger = LoggerFactory()
        self.resultsConverter = ResultsConverter()

    def async_detect(self, path):
        try:
            result = self.client.face.detect(path, False, False)
            faces = [self.resultsConverter.convert_to_coordinates_format(face) for face in result]
            return faces
        except self.client.CognitiveFaceException as exp:
            self.logger.error('Response: {}. {}'.format(exp.code, exp.msg))
