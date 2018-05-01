from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.face_detection import FaceDetection

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
