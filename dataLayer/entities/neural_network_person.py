from sqlalchemy import Column, String, Integer, Date, ForeignKey

from dataLayer.database_connection import Base


class NeuralNetworkPerson(Base):
    __tablename__ = 'NeuralNetworkPeople'

    neural_network_id = Column("NeuralNetworkId", Integer, ForeignKey("NeuralNetworks.Id"), primary_key=True)
    person_id = Column("PersonId", Integer, ForeignKey("Person.Id"), primary_key=True)
