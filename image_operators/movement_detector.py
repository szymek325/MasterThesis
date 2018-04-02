from helpers.files_manager import FilesManager
from helpers.logger_factory import LoggerFactory


class MovementDetector:

    def __init__(self):
        self.logger = LoggerFactory()
        self.filesManager = FilesManager()
