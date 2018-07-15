from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.neural_network_type import NeuralNetworkType


class NeuralNetworkTypeRepository():

    def get_all_types(self):
        Base.metadata.create_all(engine)
        session = Session()
        files = session.query(NeuralNetworkType)
        session.close()
        return files
