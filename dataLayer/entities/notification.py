from sqlalchemy import Column, String, Integer, Date
from sqlalchemy.orm import relationship

from dataLayer.database_connection import Base


class Notification(Base):
    __tablename__ = 'Notification'

    id = Column('Id', Integer, primary_key=True)
    message = Column('Message', String)
    notificationTypeId = Column('NotificationTypeId', Integer)
    image = relationship('ImageAttachment', uselist=False)

    def __init__(self, message, notificaion_type_id, image):
        self.message = message
        self.notificationTypeId = notificaion_type_id
        self.image = image
