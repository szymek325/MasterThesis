from sqlalchemy import Column, String, Integer, Date
from sqlalchemy.orm import relationship

from dataLayer.database_connection import Base


class Person(Base):
    __tablename__ = 'Person'

    id = Column('Id', Integer, primary_key=True)
    name = Column('Name', String)
    guid = Column('Guid', String)
    files = relationship("File")
    neural_network_people = relationship("NeuralNetworkPerson")
