import datetime

from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.neural_network import NeuralNetwork


class NeuralNetworkRepository():

    def get_all_not_completed(self):
        Base.metadata.create_all(engine)
        session = Session()
        requests = session.query(NeuralNetwork).filter_by(statusId=1)
        session.close()
        return requests

    def complete_request(self, id):
        Base.metadata.create_all(engine)
        session = Session()
        requests = session.query(NeuralNetwork).filter_by(id=id)
        for req in requests:
            req.statusId = 3
            req.completionTime = datetime.datetime.now()
        session.commit()
        session.close()

    def get_neural_networks_ids_with_files_count(self):
        result = []
        Base.metadata.create_all(engine)
        session = Session()
        neural_networks = session.query(NeuralNetwork)
        session.close()
        for nn in neural_networks:
            result.append([nn.id, len(nn.files)])
        return result

    def complete_as_error(self, id):
        Base.metadata.create_all(engine)
        session = Session()
        requests = session.query(NeuralNetwork).filter_by(id=id)
        for req in requests:
            req.statusId = 4
            req.completionTime = datetime.datetime.now()
        session.commit()
        session.close()
