from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.sensors_reading import SensorsReading


class SensorsReadingRepository():

    def add_reading(self, temperature_reading, humidity_reading):
        Base.metadata.create_all(engine)
        session = Session()
        reading = SensorsReading(humidity_reading, temperature_reading)
        session.add(reading)
        session.commit()
        session.close()
