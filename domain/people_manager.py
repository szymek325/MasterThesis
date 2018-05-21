from configuration_global.logger_factory import LoggerFactory
from domain.directory_manager import DirectoryManager


class PeopleManager():
    def __init__(self):
        self.logger = LoggerFactory()
        self.directoryManager = DirectoryManager()

    def check_if_people_exists_locally(self, ids: [int]):
        for person_id in ids:
            self.logger.info(person_id)
        self.logger.info("check_if_people_exists_locally")

    def download_person(self):
        self.logger.info("download_person")
