from configuration_global.logger_factory import LoggerFactory
from dataLayer.repositories.notification_repository import NotificationRepository
from dataLayer.repositories.notification_settings_repository import NotificationSettingsRepository
from dataLayer.type_providers.notification_settings_types import NotificationSettingsTypes

notification_message = "{param} value is to {direction}. Required value should fit between {min} and {max}"


class NotificationsService():
    def __init__(self):
        self.logger = LoggerFactory()
        self.notificationRepository = NotificationRepository()
        self.settingsRepository = NotificationSettingsRepository()
        self.settingsTypes = NotificationSettingsTypes()
        self.settings = {}
        self.tempSettings = {}
        self.humSettings = {}

    def add_reading_notification_if_required(self, readings_dictionary):
        self.logger.info("Checking if any notification needs to be created based on threshold values from  db")
        self.__get_settings__()
        self.__process_reading__(readings_dictionary, self.settingsTypes.temperature)
        self.__process_reading__(readings_dictionary, self.settingsTypes.humidity)

    def __get_settings__(self):
        self.settings = self.settingsRepository.get_all()

    def __process_reading__(self, dict, parameter):
        parameter_settings = self.settings.filter_by(name=parameter).one()
        reading = dict[parameter]
        if parameter_settings.min > reading:
            self.notificationRepository.add_sensor_notification(
                notification_message.format(param=parameter, direction='low', min=parameter_settings.min, max=parameter_settings.max))
        elif parameter_settings.max < reading:
            self.notificationRepository.add_sensor_notification(
                notification_message.format(param=parameter, direction='high', min=parameter_settings.min, max=parameter_settings.max))
