from sqlalchemy import Column, String, Integer, Date

from dataLayer.database_connection import Base


class FaceDetection(Base):
    __tablename__ = 'FaceDetection'

    id = Column(Integer, primary_key=True)
    dnnFaces = Column('DnnFaces', Integer)
    haarFaces = Column('HaarFaces', Integer)
    name = Column('Name', String)
    statusId = Column('StatusId', Integer)
