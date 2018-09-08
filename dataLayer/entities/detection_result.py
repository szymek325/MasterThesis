from sqlalchemy import Column, String, Integer, Date, ForeignKey
from sqlalchemy.orm import relationship

from dataLayer.database_connection import Base
import numpy as np

from dataLayer.entities.detection_rectangle import DetectionRectangle


class DetectionResult(Base):
    __tablename__ = 'DetectionResult'

    id = Column('Id', Integer, primary_key=True)
    detection_id = Column("DetectionId", Integer, ForeignKey("Detection.Id"), nullable=True)
    detection = relationship("Detection")
    detection_type_id = Column("DetectionTypeId", Integer)
    image = relationship("ImageAttachment", uselist=False)
    processing_time = Column('ProcessingTime', String)
    face_rectangles = relationship(DetectionRectangle)

    def __init__(self, request_id, detection_type_id, image_attachment, face_coordinates, time=""):
        self.detection_id = request_id
        self.detection_type_id = detection_type_id
        self.image = image_attachment
        self.face_rectangles = face_coordinates
        self.processing_time = time
