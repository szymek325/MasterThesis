import datetime

from sqlalchemy import Column, String, Integer, Date, ForeignKey
from sqlalchemy.orm import relationship

from dataLayer.database_connection import Base


class NeuralNetworkFile(Base):
    __tablename__ = 'NeuralNetworkFile'

    id = Column('Id', Integer, primary_key=True)
    name = Column('Name', String)
    creationTime = Column('CreationTime', Date)
    neuralNetworkId = Column("NeuralNetworkId", Integer, ForeignKey("NeuralNetwork.Id"))
    neuralNetworkTypeId = Column('NeuralNetworkTypeId', Integer, ForeignKey("NeuralNetworkType.Id"))
    neuralNetworkType = relationship("NeuralNetworkType", back_populates="neuralNetworkFile")

    def __init__(self, name, neural_network_id, neural_network_type_id):
        self.name = name
        self.neuralNetworkId = neural_network_id
        self.neuralNetworkTypeId = neural_network_type_id
        self.creationTime = datetime.datetime.now()
