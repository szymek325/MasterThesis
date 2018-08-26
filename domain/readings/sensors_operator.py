from configuration_global.logger_factory import LoggerFactory
from dataLayer.repositories.sensors_reading_repository import SensorsReadingRepository
from dataLayer.type_providers.notification_settings_types import NotificationSettingsTypes
from domain.notifications.notifications_service import NotificationsService
from raspberry_modules.dht11_reader_cheater import Dht11ReaderCheater


class SensorsOperator():
    def __init__(self):
        self.logger = LoggerFactory()
        self.readingsRepo = SensorsReadingRepository()
        self.dht11Reader = Dht11ReaderCheater()
        self.notifications = NotificationsService()
        self.settingsTypes = NotificationSettingsTypes()

    def add_readings(self):
        self.logger.info(f"Starting retriving readings")
        temp, hum = self.dht11Reader.read_values()
        self.logger.info(f"Temp: {temp}\n Hum: {hum}")
        self.readingsRepo.add_reading(temp, hum)
        readings_dictionary = {self.settingsTypes.temperature: temp, self.settingsTypes.humidity: hum}
        self.notifications.add_reading_notification_if_required(readings_dictionary)
        self.logger.info(f"Sensors readings finished")
