from entities.sensors_reading import SensorsReading
from helpers.database_connection import Base, engine, Session

Base.metadata.create_all(engine)
session = Session()

# update
# reading = SensorsReading(15, 29)
#
# session.add(reading)
# session.commit()
# session.close()

# read
readings = session.query(SensorsReading).all()
print('\n### All readings:')
for reading in readings:
    print(f"{reading.id} {reading.humidity} {reading.temperature}")
print('dupa')
session.close()
