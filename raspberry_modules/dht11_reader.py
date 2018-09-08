
from configuration_global.logger_factory import LoggerFactory


class Dht11Reader:
    def __init__(self):
        self.logger = LoggerFactory()
        gpio.setmode(gpio.BCM)

    def get_readings(self):
        self.logger.info(f"Reading value from dht11")
        try:
            self.logger.info(f"read value here")
        except Exception as ex:
            self.logger.error(f"Error '{ex}' when reading value from DHT11")
            # read physical value from dht11
