from configuration_global.logger_factory import LoggerFactory


class NotificationsService():
    def __init__(self):
        self.logger = LoggerFactory()

    def add_reading_notification_if_required(self):
        self.logger.info("checking if something needs to be notified about")
