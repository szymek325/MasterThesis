import os

from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from dataLayer.entities.neural_network import NeuralNetwork
from people.people_downloader import PeopleDownloader


class NeuralNetworkRequestsManager():
    def __init__(self):
        self.config = ConfigReader()
        self.logger = LoggerFactory()
        self.peopleDownloader = PeopleDownloader()

    def process_request(self, request: NeuralNetwork):
        self.peopleDownloader.get_all_required_people_to_local(request.id)

