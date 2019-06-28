from sqlalchemy import Column, String, Integer, Date

from dataLayer.database_connection import Base


class SensorsReading(Base):
    __tablename__ = 'SensorsReading'

    id = Column(Integer, primary_key=True)
    humidity = Column('Humidity', Integer)
    temperature = Column('Temperature', Integer)

    def __init__(self, humidity, temperature):
        self.humidity = humidity
        self.temperature = temperature
