from sqlalchemy import Column, String, Integer, Date, ForeignKey

from dataLayer.database_connection import Base


class RecognitionImage(Base):
    __tablename__ = 'RecognitionImage'

    id = Column(Integer, primary_key=True)
    name = Column('Name', String)
    thumbnail = Column("Thumbnail", String)
    recognition_id = Column("RecognitionId", Integer, ForeignKey("Recognition.Id"))

    def __init__(self, name, recognition_id):
        self.name = name
        self.recognition_id = recognition_id
