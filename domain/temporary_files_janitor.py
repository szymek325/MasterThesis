from configuration_global.config_reader import ConfigReader
from configuration_global.exception_handler import exception
import shutil


class TemporaryFilesJanitor():
    def __init__(self):
        self.config = ConfigReader()

    @exception
    def clean_face_detection_requests(self):
        shutil.rmtree(self.config.face_detection_requests_path)
