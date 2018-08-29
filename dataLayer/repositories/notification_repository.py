from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.notification import Notification


class NotificationRepository():
    def add_sensor_notification(self, notification_message):
        Base.metadata.create_all(engine)
        session = Session()
        notification = Notification(notification_message)
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
