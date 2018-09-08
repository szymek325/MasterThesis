import time

from cognitive_face_client.clients.azure_large_groups_client import AzureLargeGroupsClient
from configuration_global.logger_factory import LoggerFactory
from dataLayer.entities.neural_network_file import NeuralNetworkFile
from dataLayer.repositories.neural_network_file_repository import NeuralNetworkFileRepository
from dataLayer.type_providers.neural_network_types import NeuralNetworkTypes


class AzureLargeGroupTrainer():
    def __init__(self):
        self.logger = LoggerFactory()
        self.largeGroupClient = AzureLargeGroupsClient()
        self.nnFilesRepo = NeuralNetworkFileRepository()
        self.nnTypes = NeuralNetworkTypes()

    def train_large_group(self, request_id, name, people_with_image_paths):
        try:
            start = time.time()
            self.largeGroupClient.create_large_group(request_id, name)
            people_ids = set(person_id for person_id, image_path in people_with_image_paths)
            people_dictionary = {}
            for person_id in people_ids:
                person_in_group_id = self.largeGroupClient.create_person_in_large_group(request_id, person_id)
                people_dictionary[f'{person_id}'] = person_in_group_id
            for person_id, image_path in people_with_image_paths:
                group_person_id = people_dictionary[f'{person_id}']
                self.largeGroupClient.add_face_to_person_in_large_group(group_person_id, request_id, image_path)
            self.largeGroupClient.train_large_group(request_id)
            end = time.time()
            neural_network_file_entity = NeuralNetworkFile(str(request_id), request_id,
                                                           self.nnTypes.azure_large_group_id, end - start, '1>',
                                                           str(people_dictionary))
            # people dictionary may become problematic when group is very big
            self.nnFilesRepo.add_neural_network_file(neural_network_file_entity)

        except Exception as ex:
            self.logger.error(f"Exception when creating Azure Large Group. Exception: {ex}")
