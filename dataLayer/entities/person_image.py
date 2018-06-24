from sqlalchemy import Column, String, Integer, Date, ForeignKey

from dataLayer.database_connection import Base


class PersonImage(Base):
    __tablename__ = 'PersonImage'

    id = Column(Integer, primary_key=True)
    name = Column('Name', String)
    thumbnail = Column("Thumbnail", String)
    person_id = Column("PersonId", Integer, ForeignKey("Person.Id"))

    def __init__(self, name, person_id):
        self.name = name
        self.person_id = person_id
