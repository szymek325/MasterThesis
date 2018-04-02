import logging
import datetime


class LoggerFactory:

    def __init__(self):
        self.logger = logging.getLogger('FaceRecognitionLogger')
        fileHandler= logging.FileHandler(f'Logs/logs_{datetime.date.today()}.log')
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
