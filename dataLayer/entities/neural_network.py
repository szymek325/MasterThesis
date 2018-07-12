from sqlalchemy import Column, String, Integer, Date
from sqlalchemy.orm import relationship

from dataLayer.database_connection import Base
from dataLayer.entities.neural_network_file import NeuralNetworkFile


class NeuralNetwork(Base):
    __tablename__ = 'NeuralNetwork'

    id = Column('Id', Integer, primary_key=True)
    name = Column('Name', String)
    statusId = Column('StatusId', Integer)
    completionTime = Column('CompletionTime', Date)
    images = relationship(NeuralNetworkFile)
