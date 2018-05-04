from configuration_global.config_reader import ConfigReader
from configuration_global.exception_handler import exception
from configuration_global.logger_factory import LoggerFactory
from dataLayer.repositories.sensors_reading_repository import SensorsReadingRepository
from domain.gpio_manager import GpioManager


class SensorReadingsProcess():
    def __init__(self):
        self.config = ConfigReader()
        self.logger = LoggerFactory()
        self.gpio_manager = GpioManager()
        self.sensors_reading_repository = SensorsReadingRepository()

    @exception
    def run_readings(self):
        hum = self.gpio_manager.get_humidity()
        temp = self.gpio_manager.get_temperature()
        self.logger.info(f"Temperature : {temp} \nHumidity {hum}")
        self.sensors_reading_repository.add_reading(temp, hum)


if __name__ == "__main__":
    runner = SensorReadingsProcess()
    runner.run_readings()
