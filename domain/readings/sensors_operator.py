from configuration_global.logger_factory import LoggerFactory
from dataLayer.repositories.sensors_reading_repository import SensorsReadingRepository
from raspberry_modules.dht11_reader_cheater import Dht11ReaderCheater


class SensorsOperator():
    def __init__(self):
        self.logger = LoggerFactory()
        self.readingsRepo = SensorsReadingRepository()
        self.dht11Reader = Dht11ReaderCheater()

    def add_readings(self):
        self.logger.info(f"Starting retriving readings")
        temp, hum = self.dht11Reader.read_values()
        self.logger.info(f"Temp: {temp}\n Hum: {hum}")
        self.readingsRepo.add_reading(temp, hum)
        self.logger.info(f"Sensors readings finished")