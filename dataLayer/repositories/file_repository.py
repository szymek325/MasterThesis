from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.file import File


class FileRepository():

    def add_file(self, name, file_detection_id, path):
        Base.metadata.create_all(engine)
        session = Session()
        reading = File(name, file_detection_id, path)
        session.add(reading)
        session.commit()
        session.close()
