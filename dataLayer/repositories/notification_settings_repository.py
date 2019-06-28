from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.notification_settings import NotificationSettings


class NotificationSettingsRepository():

    def get_by_id(self, settings_id):
        Base.metadata.create_all(engine)
        session = Session()
        result = session.query(NotificationSettings).filter_by(id=settings_id)
        session.close()
        return result

    def get_all(self):
        Base.metadata.create_all(engine)
        session = Session()
        result = session.query(NotificationSettings)
        session.close()
        return result
