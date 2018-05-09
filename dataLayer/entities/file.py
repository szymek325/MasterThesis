from sqlalchemy import Column, String, Integer, Date, ForeignKey

from dataLayer.database_connection import Base


class File(Base):
    __tablename__ = 'File'

    id = Column(Integer, primary_key=True)
    name = Column('Name', String)
    thumbnail = Column("Thumbnail", String)
    face_detection_guid = Column("FaceDetectionGuid", String,ForeignKey("FaceDetection.Guid"))
    path = Column("Path", String)

    def __init__(self, name, face_detection_guid):
        self.name = name
        self.face_detection_guid = face_detection_guid
