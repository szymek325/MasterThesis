from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from dropbox_integration.dropbox_client import DropboxClient


class DetectionInputProvider:
    def __init__(self):
        self.config= ConfigReader()
        self.logger=LoggerFactory()
        self.dbxClient= DropboxClient()

    def get_input_for_request(self,request_id:int):
        input_file = self.dbxClient.download_single_file(f"{self.dropbox_base_path}/{request.id}",
                                                         self.requests_path)