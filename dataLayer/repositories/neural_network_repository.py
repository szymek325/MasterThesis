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

    def get_completed_neural_networks_ids_with_downloadable_files_count(self):
        result = []
        Base.metadata.create_all(engine)
        session = Session()
        neural_networks = session.query(NeuralNetwork).filter_by(statusId=3)
        session.close()
        for nn in neural_networks:
            result.append([nn.id, len([x for x in nn.files if x.neuralNetworkTypeId < 4])])
        return result

    def complete_with_error(self, id):
        Base.metadata.create_all(engine)
        session = Session()
        requests = session.query(NeuralNetwork).filter_by(id=id)
        for req in requests:
            req.statusId = 4
            req.completionTime = datetime.datetime.now()
        session.commit()
        session.close()
