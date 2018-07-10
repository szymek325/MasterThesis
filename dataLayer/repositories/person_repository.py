from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.person import Person


class PersonRepository():

    def get_by_id(self, id):
        Base.metadata.create_all(engine)
        session = Session()
        person = session.query(Person).filter_by(id=id).first()
        session.close()
        return person

    def get_all(self):
        Base.metadata.create_all(engine)
        session = Session()
        people = session.query(Person)
        session.close()
        return people.all()

    def get_people_ids_with_images_count(self):
        result = []
        Base.metadata.create_all(engine)
        session = Session()
        people = session.query(Person)
        session.close()
        for p in people:
            result.append([p.id, len(p.images)])
        return result
