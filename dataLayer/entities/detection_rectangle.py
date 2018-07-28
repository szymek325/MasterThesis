from sqlalchemy import Column, String, Integer, Date, ForeignKey
from sqlalchemy.orm import relationship

from dataLayer.database_connection import Base
import numpy as np


class DetectionRectangle(Base):
    __tablename__ = 'DetectionRectangle'

    id = Column('Id', Integer, primary_key=True)
    startX = Column('StartX', Integer)
    startY = Column('StartY', Integer)
    endX = Column('EndX', Integer)
    endY = Column('EndY', Integer)
    area = Column('Area', Integer)
    detection_id = Column("DetectionResultId", Integer, ForeignKey("DetectionResult.Id"), nullable=True)
    detection_result = relationship("DetectionResult")

    def __init__(self, coordinates):
        self.__convert_to_int_if_required__(coordinates)

    def __convert_to_int_if_required__(self, coordinates):
        startX, startY, endX, endY = coordinates
        try:
            self.startX, self.startY, self.endX, self.endY = np.asscalar(startX), np.asscalar(startY), np.asscalar(endX), np.asscalar(endY)
        except AttributeError as ex:
            self.startX, self.startY, self.endX, self.endY = startX, startY, endX, endY
        self.area = (self.endX - self.startX) * (self.endY - self.startY)
