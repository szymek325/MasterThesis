from sqlalchemy import Column, String, Integer, Date

from dataLayer.database_connection import Base


class File(Base):
    __tablename__ = 'File'

    id = Column(Integer, primary_key=True)
    name = Column('Name', String)
    file_detection_id = Column('FileDetectionId', Integer)
    path = Column("Path", String)

    def __init__(self, name, file_detection_id, path):
        self.name = name
        self.file_detection_id = file_detection_id
        self.path = path
