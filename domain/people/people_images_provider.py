import os

from configuration_global.logger_factory import LoggerFactory
from configuration_global.paths_provider import PathsProvider


class PeopleImagesProvider():
    def __init__(self):
        self.pathsProvider = PathsProvider()
        self.logger = LoggerFactory()

    def get_image_paths_for_people(self, people_ids):
        result = []
        for person_id in people_ids:
            person_path = os.path.join(self.pathsProvider.local_person_image_path(), str(person_id))
            image_paths = [os.path.join(person_path, f) for f in os.listdir(person_path)]
            for imagePath in image_paths:
                result.append([person_id, imagePath])
        return result
