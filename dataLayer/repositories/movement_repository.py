from dataLayer.database_connection import Base, engine, Session


class MovementRepository():

    def add_movement(self, movement):
        Base.metadata.create_all(engine)
        session = Session()
        session.add(movement)
        session.commit()
        generated_id = movement.id
        session.close()
        return generated_id
