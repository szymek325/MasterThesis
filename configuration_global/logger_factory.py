import logging
import datetime
import os

from configuration_global.directory_manager import DirectoryManager
from configuration_global.singleton import Singleton


class LoggerFactory(metaclass=Singleton):

    def __init__(self):
        self.dir_path = os.path.dirname(os.path.realpath(__file__))
        self.logger = logging.getLogger('FaceRecognitionLogger')
        self.directory = DirectoryManager()
        self.directory.create_directory_if_doesnt_exist(f'temp/logs')
        fileHandler = logging.FileHandler(f'temp/logs/logs_{datetime.date.today()}.log')
        fileHandler.setLevel(logging.DEBUG)
        streamHandler = logging.StreamHandler()
        streamHandler.setLevel(logging.ERROR)
        formatter = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')
        fileHandler.setFormatter(formatter)
        streamHandler.setFormatter(formatter)
        self.logger.addHandler(fileHandler)
        self.logger.addHandler(streamHandler)

    def info(self, message):
        self.logger.setLevel(logging.INFO)
        self.logger.info(message)
        print(message)

    def error(self, message):
        self.logger.setLevel(logging.ERROR)
        self.logger.error(message)
