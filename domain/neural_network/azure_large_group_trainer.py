from cognitive_face_client.azure_large_groups_client import AzureLargeGroupsClient
from configuration_global.logger_factory import LoggerFactory


class AzureLargeGroupTrainer():
    def __init__(self):
        self.logger = LoggerFactory()
        self.largeGroupClient = AzureLargeGroupsClient()

    def train_large_group(self, request_id, name, people_with_image_paths):
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
