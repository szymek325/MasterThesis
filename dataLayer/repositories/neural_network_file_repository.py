from dataLayer.database_connection import Base, Session, engine
from dataLayer.entities.neural_network_file import NeuralNetworkFile


class NeuralNetworkFileRepository():

    def add_neural_network_file(self, name, neural_network_id, neural_network_type_id):
        Base.metadata.create_all(engine)
        session = Session()
        neural_network_file = NeuralNetworkFile(name, neural_network_id, neural_network_type_id)
        session.add(neural_network_file)
        session.commit()
        session.close()

    def get_all_open_cv_files_connected_to_neural_network(self, nn_id):
        Base.metadata.create_all(engine)
        session = Session()
        result = session.query(NeuralNetworkFile).filter(NeuralNetworkFile.neuralNetworkId == nn_id,
                                                         NeuralNetworkFile.neuralNetworkTypeId < 4)
        session.close()
        return result
