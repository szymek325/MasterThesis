from sqlalchemy import Column, String, Integer, Date, ForeignKey
from sqlalchemy.orm import relationship

from dataLayer.database_connection import Base
import numpy as np


class DetectionResult(Base):
    __tablename__ = 'DetectionResult'

    id = Column('Id', Integer, primary_key=True)
    detection_id = Column("DetectionId", Integer, ForeignKey("Detection.Id"), nullable=True)
    detection = relationship("Detection")
    detection_type_id = Column("DetectionTypeId", Integer)
    image = relationship("ImageAttachment", uselist=False)
    face_rectangles = relationship("DetectionRectangle")

    def __init__(self, request_id, detection_type_id, image_attachment, face_coordinats):
        self.detection_id = request_id
        self.detection_type_id = detection_type_id
        self.image = image_attachment
        self.face_rectangles = face_coordinats
