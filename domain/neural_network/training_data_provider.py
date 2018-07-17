import cv2
import os

from configuration_global.logger_factory import LoggerFactory
from configuration_global.paths_provider import PathsProvider
from dataLayer.repositories.neural_network_person_repository import NeuralNetworkPersonRepository
import numpy as np

from opencv_client.face_detection.dnn_face_detector import DnnFaceDetector
from opencv_client.face_detection.haar_face_detector import HaarFaceDetector
from opencv_client.image_converters.image_converter import ImageConverter


class TrainingDataProvider():
    def __init__(self):
        self.logger = LoggerFactory()
        self.pathsProvider = PathsProvider()
        self.neuralNetworkPersonRepo = NeuralNetworkPersonRepository()
        self.faceDetector = DnnFaceDetector()
        self.imageConverter = ImageConverter()

    def get_training_data_for_neural_network(self, request_id: int):
        self.logger.info(f"Preparing training data for NeuralNetwork request : {request_id}")
        people_ids = self.neuralNetworkPersonRepo.get_all_people_connected_to_neural_network(request_id)
        face_samples = []
        ids = []
        for person_id in people_ids:
            person_path = os.path.join(self.pathsProvider.local_person_image_path(), str(person_id))
            image_paths = [os.path.join(person_path, f) for f in os.listdir(person_path)]
            for imagePath in image_paths:
                self.extract_training_data(face_samples, ids, imagePath, person_id)
        self.logger.info(f"face_samples:\n {face_samples}"
                         f"\nids: {ids}")
        self.logger.info(f"Preparing training data for NeuralNetwork request : {request_id} FINISHED")
        return face_samples, np.array(ids)

    def extract_training_data(self, face_samples, ids, imagePath, person_id):
        self.logger.info(imagePath)
        open_cv_image = cv2.imread(imagePath)
        faces=[]
        try:
            faces = self.faceDetector.run_detector_with_load(imagePath)
        except:
            self.logger.info(f"Exception when extracting data from {imagePath}")
        if len(faces) is not 0:
            (startX, startY, endX, endY) = faces[0]
            np_image = self.imageConverter.convert_to_np_array(open_cv_image[startY:endY, startX:endX])
            face_samples.append(np_image)
            ids.append(person_id)
