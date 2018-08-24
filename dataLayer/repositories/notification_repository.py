from dataLayer.database_connection import Base, engine, Session


class NotificationRepository():

    def add_notification(self, notification):
        Base.metadata.create_all(engine)
        session = Session()
        session.add(notification)
        session.commit()
        session.close()
