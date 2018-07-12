from sqlalchemy import Column, String, Integer, Date, ForeignKey

from dataLayer.database_connection import Base


class NeuralNetworkFile(Base):
    __tablename__ = 'NeuralNetworkFile'

    id = Column('Id', Integer, primary_key=True)
    name = Column('Name', String)
    creationTime = Column('CompletionTime', Date)
    neuralNetworkId = Column("NeuralNetworkId", Integer, ForeignKey("NeuralNetwork.Id"))
    neuralNetworkTypeId = Column('NeuralNetworkTypeId', Integer)

    def __init__(self, name, neural_network_id, neural_network_type_id):
        self.name = name
        self.neuralNetworkId = neural_network_id
        self.neuralNetworkTypeId = neural_network_type_id
