import random

from configuration_global.logger_factory import LoggerFactory


class Dht11ReaderCheater():
    def __init__(self):
        self.logger = LoggerFactory()

    def read_values(self):
        temp = random.randint(0, 50)
        humidity = random.randint(20, 80)
        return temp, humidity
