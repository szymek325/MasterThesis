from sqlalchemy import Column, String, Integer, Date, ForeignKey

from dataLayer.database_connection import Base
from dataLayer.type_providers.image_attachment_types import ImageAttachmentTypes


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

    def __init__(self, name, parent_id, type_id):
        attachment_Types= ImageAttachmentTypes()
        self.name = name
        self.image_attachment_type_id=type_id
        if type_id is attachment_Types.detection:
            self.detection_id = parent_id
        elif type_id is attachment_Types.detection_result:
            self.detection_result_id = parent_id
        elif type_id is attachment_Types.recognition:
            self.recognition_id = parent_id
        elif type_id is attachment_Types.person:
            self.person_id = parent_id
