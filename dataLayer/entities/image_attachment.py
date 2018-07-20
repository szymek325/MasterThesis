from sqlalchemy import Column, String, Integer, Date, ForeignKey

from dataLayer.database_connection import Base


class ImageAttachment(Base):
    __tablename__ = 'ImageAttachment'

    id = Column(Integer, primary_key=True)
    name = Column('Name', String)
    thumbnail = Column("Thumbnail", String)
    detection_id = Column("DetectionId", Integer, ForeignKey("Detection.Id"), nullable=True)
    detection_result_id = Column("DetectionResultId", Integer, ForeignKey("DetectionResul.Id"), nullable=True)
    recognition_id = Column("RecognitionId", Integer, ForeignKey("Recognition.Id"), nullable=True)
    person_id = Column("PersonId", Integer, ForeignKey("Person.Id"), nullable=True)
    image_attachment_type_id = Column("ImageAttachmentTypeId", Integer)

    def __init__(self, name, parent_id):
        self.name = name
        self.detection_id = parent_id
