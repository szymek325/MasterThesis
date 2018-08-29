from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.image_attachment import ImageAttachment
from dataLayer.entities.notification import Notification
from dataLayer.type_providers.image_attachment_types import ImageAttachmentTypes
from dataLayer.type_providers.notification_types import NotificationTypes


class NotificationRepository():
    def __init__(self):
        self.notificationTypes = NotificationTypes()
        self.attachmentTypes = ImageAttachmentTypes()

    def add_sensor_notification(self, notification_message):
        Base.metadata.create_all(engine)
        session = Session()
        image = ImageAttachment("no_file", self.attachmentTypes.motion_id)
        notification = Notification(notification_message, self.notificationTypes.sensor_reading, image)
        session.add(notification)
        session.commit()
        session.close()

    def add_movement_notification(self, notification_entity):
        Base.metadata.create_all(engine)
        session = Session()
        session.add(notification_entity)
        session.commit()
        generated_id = notification_entity.id
        session.close()
        return generated_id
