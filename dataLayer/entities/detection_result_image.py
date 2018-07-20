from sqlalchemy import Column, String, Integer, Date, ForeignKey

from dataLayer.database_connection import Base


class DetectionResultImage(Base):
    __tablename__ = 'DetectionResultImage'

    id = Column(Integer, primary_key=True)
    name = Column('Name', String)
    thumbnail = Column("Thumbnail", String)
    detection_id = Column("DetectionId", Integer, ForeignKey("Detection.Id"))

    def __init__(self, name, detection_id):
        self.name = name
        self.detection_id = detection_id
