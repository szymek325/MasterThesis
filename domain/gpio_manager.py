from random import randint

from configuration_global.config_reader import ConfigReader

from configuration_global.logger_factory import LoggerFactory


class GpioManager():
    def __init__(self):
        self.config = ConfigReader()
        self.logger = LoggerFactory()

    def get_temperature(self):
        return randint(18, 35)

    def get_humidity(self):
        return randint(0, 100)
