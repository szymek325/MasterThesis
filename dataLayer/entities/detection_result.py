from sqlalchemy import Column, String, Integer, Date, ForeignKey
from sqlalchemy.orm import relationship

from dataLayer.database_connection import Base
import numpy as np


class DetectionResult(Base):
    __tablename__ = 'DetectionResult'

    id = Column('Id', Integer, primary_key=True)
    startX = Column('StartX', Integer)
    startY = Column('StartY', Integer)
    endX = Column('EndX', Integer)
    endY = Column('EndY', Integer)
    detection_id = Column("DetectionId", Integer, ForeignKey("Detection.Id"), nullable=True)
    detection = relationship("Detection")
    detection_type_id = Column("DetectionTypeId", Integer)
    image = relationship("ImageAttachment", uselist=False)

    def __init__(self, coordinates, request_id, detection_type_id, image_attachment):
        startX, startY, endX, endY = coordinates
        self.startX, self.startY, self.endX, self.endY = np.asscalar(startX), np.asscalar(startY), np.asscalar(
            endX), np.asscalar(endY)
        self.detection_id = request_id
        self.detection_type_id = detection_type_id
        self.image = image_attachment
