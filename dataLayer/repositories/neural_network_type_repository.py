from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.neural_network_type import NeuralNetworkType


class NeuralNetworkTypeRepository():

    def get_all_types(self):
        Base.metadata.create_all(engine)
        session = Session()
        files = session.query(NeuralNetworkType)
        session.close()
        return files

    def get_id_by_name(self, type_name: str):
        Base.metadata.create_all(engine)
        session = Session()
        type_id = session.query(NeuralNetworkType).filter_by(name=type_name).first()
        session.close()
        return type_id.id
