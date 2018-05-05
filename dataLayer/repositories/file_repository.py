from configuration_global.exception_handler import exception
from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.file import File


class FileRepository():
    @exception
    def add_file(self, name, face_detection_guid):
        Base.metadata.create_all(engine)
        session = Session()
        reading = File(name, face_detection_guid)
        session.add(reading)
        session.commit()
        session.close()
