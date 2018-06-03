import cv2

from configuration_global.logger_factory import LoggerFactory


class NeuralNetworkCreator():
    def __init__(self):
        self.logger = LoggerFactory()
        self.recognizer = "empty"

    def create_neural_network(self, file_path):
        file_name = file_path.split('\\')[-1]
        nn_type = file_name.split('_')[1].split('.')[0].lower()
        self.logger.info(file_path)
        self.logger.info(nn_type)
        if nn_type == 'eigen':
            self.recognizer = cv2.face.EigenFaceRecognizer_create()
        elif nn_type == 'lbph':
            self.recognizer = cv2.face.LBPHFaceRecognizer_create()
        elif nn_type == 'fisher':
            self.recognizer = cv2.face.FisherFaceRecognizer_create()
        self.recognizer.read(file_path)
        return self.recognizer
