from sqlalchemy import Column, String, Integer

from dataLayer.database_connection import Base


class NotificationSettings(Base):
    __tablename__ = 'NotificationSettings'

    id = Column('Id', Integer, primary_key=True)
    name = Column('Name', String)
    min = Column('Min', Integer)
    max = Column('Max', Integer)
