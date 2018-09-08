from sqlalchemy import Column, String, Integer, Date
from sqlalchemy.orm import relationship

from dataLayer.database_connection import Base


class Movement(Base):
    __tablename__ = 'Movement'

    id = Column('Id', Integer, primary_key=True)
    message = Column('Message', String)
    image = relationship('ImageAttachment', uselist=False)

    def __init__(self, message, image):
        self.message = message
        self.image = image
