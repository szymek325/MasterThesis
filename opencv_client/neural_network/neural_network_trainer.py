import os

import cv2
import numpy
from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from configuration_global.paths_provider import PathsProvider
from domain.directory_manager import DirectoryManager


class NeuralNetworkTrainer():
    def __init__(self):
        self.logger = LoggerFactory()
        self.config = ConfigReader()
        self.directoryManager = DirectoryManager()
        self.pathsProvider = PathsProvider()
        self.requestPath = "path"

    def create_lbph_face_recognizer(self, request_id, face_samples, people_ids: [int]):
        recognizer = cv2.face.LBPHFaceRecognizer_create()
        recognizer.train(face_samples, people_ids)
        recognizer.write(f'{self.requestPath}/{request_id}_LBPH.yml')

    def create_eigen_face_recognizer(self, request_id, face_samples, people_ids: [int]):
        recognizer = cv2.face.EigenFaceRecognizer_create()
        recognizer.train(face_samples, people_ids)
        recognizer.write(f'{self.requestPath}/{request_id}_Eigen.yml')

    def create_fisher_face_recognizer(self, request_id, face_samples, people_ids: [int]):
        recognizer = cv2.face.FisherFaceRecognizer_create()
        recognizer.train(face_samples, people_ids)
        recognizer.write(f'{self.requestPath}/{request_id}_Fisher.yml')

    def create_all_face_recognizers(self, request_id, face_samples, people_ids: [int]):
        self.requestPath = os.path.join(self.pathsProvider.local_neural_network_path(), str(request_id))
        self.directoryManager.create_directory_if_doesnt_exist(self.requestPath)
        self.create_lbph_face_recognizer(request_id, face_samples, people_ids)
        self.create_eigen_face_recognizer(request_id, face_samples, people_ids)
        if len(numpy.unique(people_ids)) > 1:
            self.create_fisher_face_recognizer(request_id, face_samples, people_ids)
