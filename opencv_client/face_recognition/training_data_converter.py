import cv2
import os
import numpy as np

from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from opencv_client.face_detection.haar_face_detector import HaarFaceDetector
from opencv_client.image_converters.image_converter import ImageConverter


class TrainingDataConverter():
    def __init__(self):
        self.logger = LoggerFactory()
        self.config = ConfigReader()
        self.peoplePath = self.config.local_people_path
        self.faceDetector = HaarFaceDetector()
        self.imageConverter = ImageConverter()

    def get_training_data(self, people_ids):
        """

        :rtype: array of face samples, samples people id
        """
        self.logger.info("preparing training data")
        face_samples = []
        ids = []
        for person_id in people_ids:
            person_path = os.path.join(self.peoplePath, f"{person_id}")
            image_paths = [os.path.join(person_path, f) for f in os.listdir(person_path)]
            for imagePath in image_paths:
                open_cv_image = cv2.imread(imagePath)
                faces = self.faceDetector.run_detector(open_cv_image)
                if len(faces) is not 0:
                    for (startX, startY, endX, endY) in faces:
                        np_image = self.imageConverter.convert_to_np_array(open_cv_image[startY:endY, startX:endX])
                        face_samples.append(np_image)
                        ids.append(person_id)
                        self.logger.info('adding sample to learning array')
        return face_samples, np.array(ids)
