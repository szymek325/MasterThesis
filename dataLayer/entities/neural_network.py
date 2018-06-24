from sqlalchemy import Column, String, Integer, Date
from sqlalchemy.orm import relationship

from dataLayer.database_connection import Base


class NeuralNetwork(Base):
    __tablename__ = 'NeuralNetworks'

    id = Column('Id', Integer, primary_key=True)
    name = Column('Name', String)
    statusId = Column('StatusId', Integer)
