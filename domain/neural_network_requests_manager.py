from sqlalchemy import null

from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from dataLayer.entities.neural_network import NeuralNetwork
from dataLayer.repositories.neural_network_person_repository import NeuralnetworkPersonRepository
from dataLayer.repositories.person_repository import PersonRepository
from domain.directory_manager import DirectoryManager
from domain.people_manager import PeopleManager
from dropbox_integration.dropbox_client import DropboxClient


class NeuralNetworkRequestsManager():
    def __init__(self):
        self.config = ConfigReader()
        self.dbxClient = DropboxClient()
        self.directory = DirectoryManager()
        self.logger = LoggerFactory()
        self.nnpRepo = NeuralnetworkPersonRepository()
        self.peopleManager = PeopleManager()

    def process_request(self, request: NeuralNetwork):
        self.logger.info(f"Getting all people added to request id : {request.id}")
        connections = self.nnpRepo.get_all_connected_to_request(request.id)
        peoplesId = []
        for connection in connections:
            peoplesId.append(connection.person_id)
            self.logger.info(connection)

        self.peopleManager.check_if_people_exists_locally(peoplesId)

        self.logger.info(peoplesId)
