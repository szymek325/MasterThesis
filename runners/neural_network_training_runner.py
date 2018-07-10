from sqlalchemy import null

from configuration_global.logger_factory import LoggerFactory
from dataLayer.repositories.neural_network_repository import NeuralNetworkRepository
from domain.neural_network.neural_network_requests_manager import NeuralNetworkRequestsManager
from domain.people.people_downloader import PeopleDownloader


class NeuralNetworkTrainingRunner():
    def __init__(self):
        self.logger = LoggerFactory()
        self.peopleManager = PeopleDownloader()
        self.nnManager = NeuralNetworkRequestsManager()
        self.nnRepo = NeuralNetworkRepository()

    def process(self):
        self.peopleManager.download_people_to_local()
        requests = self.nnRepo.get_all_not_completed()
        if not requests == null:
            for request in requests:
                self.nnManager.process_request(request)
        self.logger.info("Processing done")


if __name__ == "__main__":
    drop = NeuralNetworkTrainingRunner()
    drop.process()
