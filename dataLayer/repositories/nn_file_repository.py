from configuration_global.exception_handler import exception
from dataLayer.database_connection import Base, Session, engine
from dataLayer.entities.neural_network_file import NeuralNetworkFile


class NeuralNetworkFileRepository():

    @exception
    def add_detection_file(self, name, neural_network_id, neural_network_type_id):
        Base.metadata.create_all(engine)
        session = Session()
        neural_network_file = NeuralNetworkFile(name, neural_network_id, neural_network_type_id)
        session.add(neural_network_file)
        session.commit()
        session.close()
