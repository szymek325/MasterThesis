from sqlalchemy import Column, String, Integer, Date

from dataLayer.database_connection import Base


class Notification(Base):
    __tablename__ = 'Notification'

    id = Column('Id', Integer, primary_key=True)
    message = Column('Message', String)

    def __init__(self, message):
        self.message = message
