import os

from sqlalchemy import null

from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from dataLayer.repositories.person_repository import PersonRepository
from domain.directory_manager import DirectoryManager
from dropbox_integration.dropbox_client import DropboxClient


class PeopleManager():
    def __init__(self):
        self.logger = LoggerFactory()
        self.directoryManager = DirectoryManager()
        self.config = ConfigReader()
        self.peopleRepo = PersonRepository()
        self.peoplePath = self.config.local_people_path
        self.dropboxClient = DropboxClient()

    def check_if_people_exists_locally(self, ids: [int]):
        for person_id in ids:
            path = os.path.join(self.peoplePath, f"{person_id}")
            if not os.path.exists(path):
                person = self.peopleRepo.get_by_id(person_id)
                if person is not null:
                    self.logger.info("need to download person")
                    self.dropboxClient.download_folder(person.guid, path)

    def download_person(self):
        self.logger.info("download_person")
