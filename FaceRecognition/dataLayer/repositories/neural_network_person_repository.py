from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.neural_network_person import NeuralNetworkPerson


class NeuralNetworkPersonRepository():

    def get_all_people_connected_to_neural_network(self, nn_id):
        result = []
        Base.metadata.create_all(engine)
        session = Session()
        people = session.query(NeuralNetworkPerson).filter_by(neural_network_id=nn_id)
        session.close()
        for p in people:
            result.append(p.person_id)
        return result
