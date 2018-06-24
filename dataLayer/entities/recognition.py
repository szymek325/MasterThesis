from sqlalchemy import Column, String, Integer, Date, ForeignKey
from sqlalchemy.orm import relationship

from dataLayer.database_connection import Base
from dataLayer.entities.neural_network import NeuralNetwork
from dataLayer.entities.recognition_image import RecognitionImage


class Recognition(Base):
    __tablename__ = 'Recognition'

    id = Column('Id', Integer, primary_key=True)
    name = Column('Name', String)
    neural_network_id = Column("NeuralNetworkId", Integer, ForeignKey("NeuralNetworks.Id"))
    neural_network = relationship(NeuralNetwork)
    statusId = Column('StatusId', Integer, ForeignKey("Status.Id"))
    images = relationship(RecognitionImage)
