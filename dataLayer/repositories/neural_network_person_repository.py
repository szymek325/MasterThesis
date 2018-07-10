from configuration_global.exception_handler import exception
from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.neural_network_person import NeuralNetworkPerson


class NeuralNetworkPersonRepository():

    @exception
    def get_all_people_connected_to_neural_network(self, nn_id):
        Base.metadata.create_all(engine)
        session = Session()
        requests = session.query(NeuralNetworkPerson).filter_by(neural_network_id=nn_id)
        session.close()
        return requests
