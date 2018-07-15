import datetime
from sqlalchemy import Column, String, Integer, Date, ForeignKey

from dataLayer.database_connection import Base


class NeuralNetworkType(Base):
    __tablename__ = 'NeuralNetworkType'

    id = Column('Id', Integer, primary_key=True)
    name = Column('Name', String)
    creationTime = Column('CreationTime', Date)
