import cv2

from configuration_global.logger_factory import LoggerFactory
import numpy as np

from opencv_client.face_detection.haar_face_detector import HaarFaceDetector
from opencv_client.image_converters.image_converter import ImageConverter


class TrainingDataProvider():
    def __init__(self):
        self.logger = LoggerFactory()
        self.faceDetector = HaarFaceDetector()
        self.imageConverter = ImageConverter()
        self.face_samples = []
        self.ids = []

    def get_training_data_for_neural_network(self, request_id: int, people_with_image_paths):
        self.logger.info(f"Preparing training data for NeuralNetwork request : {request_id}")
        self.face_samples.clear()
        self.ids.clear()
        for person_id, person_image in people_with_image_paths:
            self.__extract_training_data__(person_image, person_id)
        self.logger.info(f"face_samples:\n {self.face_samples}"f"\nids: {self.ids}")
        self.logger.info(f"Preparing training data for NeuralNetwork request : {request_id} FINISHED")
        return self.face_samples, np.array(self.ids)

    def __extract_training_data__(self, image_path, person_id):
        self.logger.info(image_path)
        open_cv_image = cv2.imread(image_path)
        faces = []
        try:
            faces = self.faceDetector.run_detector(open_cv_image)
        except Exception as ex:
            self.logger.info(f"Exception when extracting data from {image_path}. Ex: {ex}")
        if len(faces) is not 0:
            self.__add_sample__(faces, open_cv_image, person_id)

    def __add_sample__(self, faces, open_cv_image, person_id):
        (startX, startY, endX, endY) = faces[0]
        cropped_image = open_cv_image[startY:endY, startX:endX]
        np_image = self.imageConverter.convert_to_np_array(cropped_image)
        self.face_samples.append(np_image)
        self.ids.append(person_id)
