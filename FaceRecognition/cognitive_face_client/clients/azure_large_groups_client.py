import datetime

from cognitive_face_client.clients.cognitive_client import CognitiveClient
from cognitive_face_client.results_converter import ResultsConverter
from configuration_global.logger_factory import LoggerFactory


class AzureLargeGroupsClient():
    def __init__(self):
        self.cognitiveClient = CognitiveClient()
        self.logger = LoggerFactory()
        self.resultsConverter = ResultsConverter()

    def create_large_group(self, request_id: int, name: str):
        self.logger.info(
            f"Creating Azure LargePersonGroup with id: {request_id}, name : {name} at {datetime.datetime.now()}")
        try:
            self.cognitiveClient.create_large_group(request_id, name)
        except Exception as ex:
            self.logger.error(f"Exception when creating large group. Ex: {ex}")
            raise

    def create_person_in_large_group(self, large_group_id, person_name):
        self.logger.info(f"Creating person: {person_name} in large group: {large_group_id}")
        try:
            res = self.cognitiveClient.create_person_in_large_group(large_group_id, person_name)
            person_id = res['personId']
            return person_id
        except Exception as ex:
            self.logger.error(f"Exception when creating person in large group. Ex: {ex}")
            raise

    def add_face_to_person_in_large_group(self, person_id, large_group_id, image_path):
        self.logger.info(f"Adding image {image_path} to person {person_id} in large group {large_group_id}")
        try:
            self.cognitiveClient.add_face_to_person_in_large_group(person_id, large_group_id, image_path)
        except self.cognitiveClient.client.CognitiveFaceException as ex:
            self.logger.error(f"Exception when adding face from {image_path}.Process will continue. Ex: {ex}")
            if not (ex.status_code == 400 and ex.code == 'InvalidImage'):
                raise
        except Exception as ex:
            self.logger.error(f"Exception when adding face to person in large group. ImagePath: {image_path}. Ex: {ex}")
            raise

    def train_large_group(self, large_group_id):
        self.logger.info(f"Starting training of azure large group id: {large_group_id} at: {datetime.datetime.now()}")
        try:
            self.cognitiveClient.train_large_group(large_group_id)
        except Exception as ex:
            self.logger.error(f"Exception when training large group. Ex: {ex}")
            raise

    def get_large_group_status(self, large_group_id):
        self.logger.info(f"Checking status of large group id: {large_group_id}")
        try:
            res = self.cognitiveClient.get_large_group_status(large_group_id)
            self.logger.info(f"Azure NN status :{res}")
            return res
        except Exception as ex:
            self.logger.error(f"Exception when checking large group status. Ex: {ex}")
            raise

    def get_person_in_large_group_name(self, large_group_id, person_specific_number):
        self.logger.info(f"Checking Name of person in large group :{large_group_id} under id :{person_specific_number}")
        try:
            res = self.cognitiveClient.get_person_name(large_group_id, person_specific_number)
            self.logger.info(f"Person name :{res['name']}")
            return res['name']
        except Exception as ex:
            self.logger.error(f"Exception when getting name of person in large group. Ex: {ex}")
            raise
