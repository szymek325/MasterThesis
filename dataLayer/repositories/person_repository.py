from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.person import Person


class PersonRepository():

    def get_by_id(self,id):
        Base.metadata.create_all(engine)
        session = Session()
        person = session.query(Person).filter_by(id=id).first()
        session.close()
        return person