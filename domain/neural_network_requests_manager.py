import os

from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from dataLayer.entities.neural_network import NeuralNetwork
from dataLayer.repositories.neural_network_person_repository import NeuralnetworkPersonRepository
from domain.directory_manager import DirectoryManager
from domain.face_detectors_manager import FaceDetectorsManager
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
        self.faceDetectionManager = FaceDetectorsManager()
        self.peoplePath = self.config.local_people_path

    def process_request(self, request: NeuralNetwork):
        self.logger.info(f"Getting all people added to request id : {request.id}")
        connections = self.nnpRepo.get_all_connected_to_request(request.id)
        people_ids = []
        for connection in connections:
            people_ids.append(connection.person_id)
            self.logger.info(connection)
        self.logger.info(people_ids)
        for person_id in people_ids:
            files = os.listdir(os.path.join(self.peoplePath, f"{person_id}"))
            self.logger.info(files)
        self.peopleManager.download_people_to_local_directory(people_ids)
