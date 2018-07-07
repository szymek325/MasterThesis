import os

from sqlalchemy import null

from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from configuration_global.paths_provider import PathsProvider
from dataLayer.repositories.neural_network_person_repository import NeuralnetworkPersonRepository
from dataLayer.repositories.person_repository import PersonRepository
from dropbox_integration.dropbox_client import DropboxClient


class PeopleDownloader():
    def __init__(self):
        self.config = ConfigReader()
        self.logger = LoggerFactory()
        self.dbxClient = DropboxClient()
        self.nnpRepo = NeuralnetworkPersonRepository()
        self.peopleRepo = PersonRepository()
        self.pathsProvider = PathsProvider()
        self.peoplePath = self.pathsProvider.local_person_image_path()

    def get_all_required_people_to_local(self, request_id):
        """

        :rtype: [int] - ids of all required people
        """
        connections = self.nnpRepo.get_all_connected_to_request(request_id)
        people_ids = []
        for connection in connections:
            people_ids.append(connection.person_id)
            self.logger.info(connection)
        self.logger.info(people_ids)
        self.__download_people_to_local_directory__(people_ids)
        for person_id in people_ids:
            files = os.listdir(os.path.join(self.peoplePath, f"{person_id}"))
            self.logger.info(files)
        return people_ids

    def __download_people_to_local_directory__(self, ids: [int]):
        for person_id in ids:
            path = os.path.join(self.peoplePath, f"{person_id}")
            if not os.path.exists(path):
                person = self.peopleRepo.get_by_id(person_id)
                if person is not null:
                    self.logger.info("need to download person")
                    self.dbxClient.download_folder(person.guid, path)

    def __download_person__(self, person_id):
        path = os.path.join(self.peoplePath, f"{person_id}")
        if not os.path.exists(path):
            person = self.peopleRepo.get_by_id(person_id)
            if person is not null:
                self.dbxClient.download_folder(person.guid, path)
