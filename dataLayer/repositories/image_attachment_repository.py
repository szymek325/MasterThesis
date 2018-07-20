from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.image_attachment import ImageAttachment


class ImageAttachmentRepository():
    def add_detection_result_image(self, name, detection_result_id, type_id):
        Base.metadata.create_all(engine)
        session = Session()
        image = ImageAttachment(name, detection_result_id, type_id)
        session.add(image)
        session.commit()
        session.close()
