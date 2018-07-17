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

    def run_training(self):
        self.logger.info("  START NeuralNetworkTraining")
        requests = self.nnRepo.get_all_not_completed()
        if not requests == null and requests.count() is not 0:
            self.peopleManager.download_people_to_local()
            for request in requests:
                try:
                    self.nnManager.process_request(request)
                except:
                    self.logger.error(f"Exception when processing {request.id}")
                    self.nnRepo.complete_as_error(request.id)
        self.logger.info("  END NeuralNetworkTraining")


if __name__ == "__main__":
    drop = NeuralNetworkTrainingRunner()
    drop.run_training()
