from sqlalchemy import Column, String, Integer, Date, ForeignKey
from sqlalchemy.orm import relationship

from dataLayer.database_connection import Base
from dataLayer.entities.neural_network import NeuralNetwork


class Recognition(Base):
    __tablename__ = 'Recognition'

    id = Column('Id', Integer, primary_key=True)
    name = Column('Name', String)
    neural_network_id = Column("NeuralNetworkId", Integer, ForeignKey("NeuralNetwork.Id"))
    neural_network = relationship(NeuralNetwork)
    statusId = Column('StatusId', Integer)
    image = relationship("ImageAttachment")
    completionTime = Column('CompletionTime', Date)
