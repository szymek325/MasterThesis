from cognitive_face_client.clients.cognitive_client import CognitiveClient
from configuration_global.logger_factory import LoggerFactory


class AzureLargeGroupCleaner:
    def __init__(self):
        self.logger = LoggerFactory()
        self.cfClient = CognitiveClient()

    def run_program(self):
        self.logger.info("START CleanUp")
        number = 1
        while True:
            try:
                self.logger.info(f"Deleting large group with id {number}")
                self.cfClient.delete_large_person_group(number)
                number = number + 1
            except Exception as ex:
                number = number + 1
                self.logger.error(ex)
        self.logger.info("END CleanUp")


if __name__ == "__main__":
    program = AzureLargeGroupCleaner()
    program.run_program()
