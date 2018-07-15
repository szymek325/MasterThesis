import datetime
from sqlalchemy import Column, String, Integer, Date, ForeignKey


class NeuralNetworkType():
    __tablename__ = 'NeuralNetworkType'

    id = Column('Id', Integer, primary_key=True)
    name = Column('Name', String)
    creationTime = Column('CreationTime', Date)
