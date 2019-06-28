import cv2
from configuration_global.exception_handler import exception
import os

from opencv_client.configuration.opencv_configuration_reader import OpenCvConfigurationReader


class HaarFaceDetector:

    def __init__(self):
        self.configReader = OpenCvConfigurationReader()
        self.faceCascade = cv2.CascadeClassifier(os.path.abspath(self.configReader.face_cascade_path))

    def run_detector(self, image):
        """
        :param image: loaded by cv2.imread
        :return: list of  startX, startY, endX, endY
        """

        gray_image = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
        faces = self.faceCascade.detectMultiScale(gray_image, 1.3, 5)
        faces_in_required_return_format = [(x, y, x + w, y + h) for x, y, w, h in faces]
        return faces_in_required_return_format
