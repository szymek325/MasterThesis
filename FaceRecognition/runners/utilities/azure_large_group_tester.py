from cognitive_face_client.clients.cognitive_client import CognitiveClient
from configuration_global.logger_factory import LoggerFactory


class AzureLargeGroupTester:
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

    def check_name(self):
        self.logger.info("START CleanUp")
        res = self.cfClient.get_person_name(4, 'f6a55407-1bb7-49f2-976d-a1fc58d52127')
        self.logger.info(res)
        self.logger.info("END CleanUp")

    def check_status(self):
        self.logger.info("START checkstatus")
        res = self.cfClient.get_large_group_status(8)
        self.logger.info(res)
        self.logger.info("END checkstatus")

    def get_azure_group(self):
        self.logger.info("START checkstatus")
        res = self.cfClient.get_large_group(6)
        self.logger.info(res)
        self.logger.info("END checkstatus")


if __name__ == "__main__":
    program = AzureLargeGroupTester()
    program.check_status()
