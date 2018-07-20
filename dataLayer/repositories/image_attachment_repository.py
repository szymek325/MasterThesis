from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.image_attachment import ImageAttachment


class ImageAttachmentRepository():
    def add_detection_result_image(self, name, detection_id):
        Base.metadata.create_all(engine)
        session = Session()
        reading = ImageAttachment(name, detection_id)
        session.add(reading)
        session.commit()
        session.close()
