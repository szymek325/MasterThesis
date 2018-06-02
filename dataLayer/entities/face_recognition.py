from sqlalchemy import Column, String, Integer, Date, ForeignKey
from sqlalchemy.orm import relationship

from dataLayer.database_connection import Base
from dataLayer.entities.neural_network import NeuralNetwork


class FaceRecognition(Base):
    __tablename__ = 'FaceRecognitions'

    id = Column('Id', Integer, primary_key=True)
    name = Column('Name', String)
    guid = Column('Guid', String)
    statusId = Column('StatusId', Integer)

