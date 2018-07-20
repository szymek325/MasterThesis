from sqlalchemy import Column, String, Integer, Date
from sqlalchemy.orm import relationship

from dataLayer.database_connection import Base
from dataLayer.entities.image_attachment import ImageAttachment


class Person(Base):
    __tablename__ = 'Person'

    id = Column('Id', Integer, primary_key=True)
    name = Column('Name', String)
    images = relationship(ImageAttachment)
