from sqlalchemy import Column, String, Integer, Date, ForeignKey

from dataLayer.database_connection import Base


class File(Base):
    __tablename__ = 'File'

    id = Column(Integer, primary_key=True)
    name = Column('Name', String)
    thumbnail = Column("Thumbnail", String)
    face_detection_id = Column("FaceDetectionId", Integer, ForeignKey("FaceDetection.Id"))
    person_id = Column("PersonId", Integer, ForeignKey("Person.Id"))

    def __init__(self, name, face_detection_id):
        self.name = name
        self.face_detection_id = face_detection_id
