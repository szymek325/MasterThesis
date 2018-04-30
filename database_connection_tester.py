from entities.face_detection import FaceDetection
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
readings = session.query(FaceDetection).all()
print('\n### All readings:')
for reading in readings:
    print(f"{reading.id} {reading.name} {reading.statusId}")
    reading.statusId = 2
session.commit()
print('dupa')
session.close()
