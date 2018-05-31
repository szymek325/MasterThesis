import cv2
import os
import numpy as np
from PIL import Image

from configuration_global.logger_factory import LoggerFactory
from faceDetection.haar_face_detector import HaarFaceDetector


class TrainingDataConverter():
    def __init__(self):
        self.logger = LoggerFactory()
        self.peoplePath = self.config.local_people_path
        self.faceDetector = HaarFaceDetector()

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
                pil_image = Image.open(imagePath).convert("RGB")
                opencv_image = cv2.cvtColor(np.array(pil_image), cv2.COLOR_RGB2BGR)
                faces = self.faceDetector.run_detector(opencv_image)
                if len(faces) is not 0:
                    image_np = np.array(pil_image.convert('L'), 'uint8')
                    for (startX, startY, endX, endY) in faces:
                        face_samples.append(image_np[startY:endY, startX:endX])
                        ids.append(person_id)
                        self.logger.info('adding sample to learning array')
        return face_samples, ids
