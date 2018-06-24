from sqlalchemy import Column, String, Integer, Date, ForeignKey
from sqlalchemy.orm import relationship

from dataLayer.database_connection import Base
from dataLayer.entities.file import File
from dataLayer.entities.neural_network import NeuralNetwork


class FaceRecognition(Base):
    __tablename__ = 'FaceRecognitions'

    id = Column('Id', Integer, primary_key=True)
    name = Column('Name', String)
    guid = Column('Guid', String)
    neural_network_id = Column("NeuralNetworkId", Integer, ForeignKey("NeuralNetworks.Id"))
    neural_network = relationship(NeuralNetwork)
    statusId = Column('StatusId', Integer, ForeignKey("Status.Id"))
    files = relationship(File)
