import os

from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from dataLayer.repositories.person_repository import PersonRepository
from domain.directory_manager import DirectoryManager


class PeopleManager():
    def __init__(self):
        self.logger = LoggerFactory()
        self.directoryManager = DirectoryManager()
        self.config= ConfigReader()
        self.peopleRepo= PersonRepository()

    def check_if_people_exists_locally(self, ids: [int]):
        for person_id in ids:
            if not os.path.exists(f"{self.config.people_path}"/person_id):
                self.download_person()
        self.logger.info("check_if_people_exists_locally")

    def download_person(self):
        person

