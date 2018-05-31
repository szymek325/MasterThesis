import cv2

from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from domain.directory_manager import DirectoryManager


class NeuralNetworkTrainer():
    def __init__(self):
        self.logger = LoggerFactory()
        self.config = ConfigReader()
        self.directoryManager = DirectoryManager()

    def create_lbph_face_recognizer(self, request_id, face_samples, people_ids: [int]):
        recognizer = cv2.face.LBPHFaceRecognizer_create()
        recognizer.train(face_samples, people_ids)
        self.recognizer.write(f'{self.config.openCv_files_path}/{request_id}/{request_id}_LBPH.yml')

    def create_eigen_face_recognizer(self, request_id, face_samples, people_ids: [int]):
        recognizer = cv2.face.EigenFaceRecognizer_create()
        recognizer.train(face_samples, people_ids)
        self.recognizer.write(f'{self.config.openCv_files_path}/{request_id}/{request_id}_Eigen.yml')

    def create_fisher_face_recognizer(self, request_id, face_samples, people_ids: [int]):
        recognizer = cv2.face.FisherFaceRecognizer_create()
        recognizer.train(face_samples, people_ids)
        self.recognizer.write(f'{self.config.openCv_files_path}/{request_id}/{request_id}_Fisher.yml')

    def create_all_face_recognizers(self, request_id, face_samples, people_ids: [int]):
        self.create_lbph_face_recognizer(request_id, face_samples, people_ids)
        self.create_eigen_face_recognizer(request_id, face_samples, people_ids)
        self.create_fisher_face_recognizer(request_id, face_samples, people_ids)
