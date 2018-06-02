from sqlalchemy import Column, String, Integer, Date, ForeignKey
from sqlalchemy.orm import relationship

from dataLayer.database_connection import Base


class FaceRecognition(Base):
    __tablename__ = 'FaceRecognition'

    id = Column('Id', Integer, primary_key=True)
    name = Column('Name', String)
    guid = Column('Guid', String)
    statusId = Column('StatusId', Integer)
    neural_network_id = Column("NeuralNetworkId", Integer, ForeignKey("NeuralNetworks.Id"))
    files = relationship("File")
