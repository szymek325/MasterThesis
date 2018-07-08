from configuration_global.logger_factory import LoggerFactory
from domain.people.people_manager import PeopleManager


class NeuralNetworkTrainingRunner():
    def __init__(self):
        self.logger = LoggerFactory()
        self.peopleManager = PeopleManager()

    def process(self):
        self.peopleManager.download_people_if_some_missing()


if __name__ == "__main__":
    drop = NeuralNetworkTrainingRunner()
    drop.process()
