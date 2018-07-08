from configuration_global.logger_factory import LoggerFactory
from configuration_global.paths_provider import PathsProvider
from domain.directory_manager import DirectoryManager


class PeopleManager():
    def __init__(self):
        self.logger = LoggerFactory()
        self.pathProvider = PathsProvider()
        self.directoryManager = DirectoryManager()

    def download_people_if_some_missing(self):
        people_path = self.pathProvider.local_person_image_path()
        directories = self.directoryManager.get_all_subdirectories(people_path)
        print(directories)
