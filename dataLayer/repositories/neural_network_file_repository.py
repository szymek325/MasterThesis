from configuration_global.exception_handler import exception
from dataLayer.database_connection import Base, Session, engine
from dataLayer.entities.neural_network_file import NeuralNetworkFile


class NeuralNetworkFileRepository():

    @exception
    def add_neural_network_file(self, name, neural_network_id, neural_network_type_id):
        Base.metadata.create_all(engine)
        session = Session()
        neural_network_file = NeuralNetworkFile(name, neural_network_id, neural_network_type_id)
        session.add(neural_network_file)
        session.commit()
        session.close()

    @exception
    def get_all_files_connected_to_neural_network(self, nn_id):
        Base.metadata.create_all(engine)
        session = Session()
        files = session.query(NeuralNetworkFile).filter_by(neuralNetworkId=nn_id)
        session.close()
        return files

    @exception
    def get_all_files_connected_to_neural_network_with_neural_types(self, nn_id):
        Base.metadata.create_all(engine)
        session = Session()
        files = session.query(NeuralNetworkFile).filter_by(neuralNetworkId=nn_id)
        session.close()
        return files
