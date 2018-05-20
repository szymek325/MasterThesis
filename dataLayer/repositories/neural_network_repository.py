from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.neural_network import NeuralNetwork


class NeuralNetworkRepository():

    def get_all_not_completed(self):
        Base.metadata.create_all(engine)
        session = Session()
        requests = session.query(NeuralNetwork).filter_by(statusId=1)
        session.close()
        return requests
