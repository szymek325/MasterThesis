import datetime

from sqlalchemy import Column, String, Integer, Date, ForeignKey

from dataLayer.database_connection import Base


class NeuralNetworkFile(Base):
    __tablename__ = 'NeuralNetworkFile'

    id = Column('Id', Integer, primary_key=True)
    name = Column('Name', String)
    creationTime = Column('CreationTime', Date)
    neuralNetworkId = Column("NeuralNetworkId", Integer, ForeignKey("NeuralNetwork.Id"))
    neuralNetworkTypeId = Column('NeuralNetworkTypeId', Integer)
    additional_data = Column('AdditionalData', String)

    def __init__(self, name, neural_network_id, neural_network_type_id, additional_data=""):
        self.name = name
        self.neuralNetworkId = neural_network_id
        self.neuralNetworkTypeId = neural_network_type_id
        self.creationTime = datetime.datetime.now()
        self.additional_data = additional_data
