import os
import time

import cv2
import numpy
from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from configuration_global.paths_provider import PathsProvider
from dataLayer.type_providers.neural_network_types import NeuralNetworkTypes
from domain.directory_manager import DirectoryManager


class NeuralNetworkTrainer():
    def __init__(self):
        self.logger = LoggerFactory()
        self.config = ConfigReader()
        self.directoryManager = DirectoryManager()
        self.pathsProvider = PathsProvider()
        self.nnTypes = NeuralNetworkTypes()
        self.requestPath = "path"

    def create_lbph_face_recognizer(self, request_id, face_samples, people_ids: [int]):
        try:
            recognizer = cv2.face.LBPHFaceRecognizer_create()
            recognizer.train(face_samples, people_ids)
            recognizer.write(f'{self.requestPath}/{request_id}_{self.nnTypes.lbph}.xml')
        except Exception as ex:
            self.logger.error(f"Exception when creating LBPH neural network. Ex: {ex}")

    def create_eigen_face_recognizer(self, request_id, face_samples, people_ids: [int]):
        try:
            recognizer = cv2.face.EigenFaceRecognizer_create()
            recognizer.train(face_samples, people_ids)
            recognizer.write(f'{self.requestPath}/{request_id}_{self.nnTypes.eigen}.xml')
        except Exception as ex:
            self.logger.error(f"Exception when creating Eigen neural network. Ex: {ex}")

    def create_fisher_face_recognizer(self, request_id, face_samples, people_ids: [int]):
        try:
            recognizer = cv2.face.FisherFaceRecognizer_create()
            recognizer.train(face_samples, people_ids)
            recognizer.write(f'{self.requestPath}/{request_id}_{self.nnTypes.fisher}.xml')
        except Exception as ex:
            self.logger.error(f"Exception when creating Fisher neural network. Ex: {ex}")

    def create_all_face_recognizers(self, request_id, training_data, data_preparation_time):
        face_samples = training_data[0]
        people_ids = training_data[1]
        self.requestPath = os.path.join(self.pathsProvider.local_neural_network_path(), str(request_id))
        self.directoryManager.create_directory_if_doesnt_exist(self.requestPath)
        start = time.time()
        self.create_lbph_face_recognizer(request_id, face_samples, people_ids)
        lbph_end = time.time()
        self.create_eigen_face_recognizer(request_id, face_samples, people_ids)
        eigen_end = time.time()
        training_times = {NeuralNetworkTypes().lbph_id: lbph_end - start + data_preparation_time,
                          NeuralNetworkTypes().eigen_id: eigen_end - lbph_end + data_preparation_time}
        if len(numpy.unique(people_ids)) > 1:
            self.create_fisher_face_recognizer(request_id, face_samples, people_ids)
            fisher_end = time.time()
            training_times[NeuralNetworkTypes().eigen_id] = fisher_end - eigen_end + data_preparation_time
        return training_times
