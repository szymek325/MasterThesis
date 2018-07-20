from sqlalchemy import Column, String, Integer, Date
from sqlalchemy.orm import relationship

from dataLayer.database_connection import Base


class Detection(Base):
    __tablename__ = 'Detection'

    id = Column('Id', Integer, primary_key=True)
    dnnFaces = Column('DnnFaces', Integer)
    haarFaces = Column('HaarFaces', Integer)
    name = Column('Name', String)
    statusId = Column('StatusId', Integer)
    image = relationship("ImageAttachment")
    completionTime = Column('CompletionTime', Date)
