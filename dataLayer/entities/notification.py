from sqlalchemy import Column, String, Integer, Date
from sqlalchemy.orm import relationship

from dataLayer.database_connection import Base
from dataLayer.type_providers.notification_types import NotificationTypes


class Notification(Base):
    __tablename__ = 'Notification'

    id = Column('Id', Integer, primary_key=True)
    message = Column('Message', String)
    image = relationship("ImageAttachment")
    notificationTypeId = Column('NotificationTypeId', Integer)

    def __init__(self, message, notificaion_type_id, image):
        self.message = message
        self.notificationTypeId = notificaion_type_id
        if notificaion_type_id == NotificationTypes.motion_detection:
            self.image = image
