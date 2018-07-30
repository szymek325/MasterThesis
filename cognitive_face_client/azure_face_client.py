from cognitive_face import CognitiveFaceException

from cognitive_face_client.cognitive_client import CognitiveClient
from cognitive_face_client.results_converter import ResultsConverter
from configuration_global.logger_factory import LoggerFactory


class AzureFaceClient():
    def __init__(self):
        self.cognitiveClient = CognitiveClient()
        self.resultsConverter = ResultsConverter()
        self.logger = LoggerFactory()

    def get_face_rectangles(self, image_path):
        self.logger.info(f"Getting face rectangles for image {image_path}")
        try:
            result = self.cognitiveClient.detect_faces(image_path)
            faces = self.resultsConverter.get_face_rectangles_from_result(result)
            return faces
        except CognitiveFaceException as exp:
            self.logger.error(f'Exception when sending face detection request. Response: {exp.code}. {exp.msg}')

    def get_face_ids(self, path):
        self.logger.info(f"Getting face ids for image {image_path}")
        try:
            result = self.cognitiveClient.detect_faces(path)
            faces = self.resultsConverter.get_face_ids_from_result(result)
            return faces
        except CognitiveFaceException as exp:
            self.logger.error(f'Exception when sending face detection request. Response: {exp.code}. {exp.msg}')

    def get_faces_identity(self, faces_ids, large_group_id):
        self.logger.info(f"Getting faces identity for faces_ids {faces_ids}")
        try:
            result = self.cognitiveClient.identify_faces(faces_ids, large_group_id)
            faces_identities = self.resultsConverter.get_face_identities_from_result(result)
            return faces_identities
        except CognitiveFaceException as exp:
            self.logger.error(f'Exception when sending identify face request. Response: {exp.code}. {exp.msg}')
