from configuration_global.exception_handler import exception
from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.detection_image import DetectionImage
from dataLayer.entities.person_image import PersonImage


class FileRepository():
    @exception
    def add_detection_file(self, name, face_detection_guid):
        Base.metadata.create_all(engine)
        session = Session()
        reading = DetectionImage(name, face_detection_guid)
        session.add(reading)
        session.commit()
        session.close()
