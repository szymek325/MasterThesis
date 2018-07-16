from configuration_global.exception_handler import exception
from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.detection_image import DetectionImage


class DetectionImageRepository():
    @exception
    def add_detection_file(self, name, detection_id):
        Base.metadata.create_all(engine)
        session = Session()
        reading = DetectionImage(name, detection_id)
        session.add(reading)
        session.commit()
        session.close()
