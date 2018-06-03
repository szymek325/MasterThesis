import cv2

from faceDetection.configuration.config_reader import ConfigReader
from configuration_global.exception_handler import exception
from domain.files_manager import FilesManager
import os


class HaarFaceDetector:

    def __init__(self):
        self.configReader = ConfigReader()
        self.filesManager = FilesManager()
        self.faceCascade = cv2.CascadeClassifier(os.path.abspath(self.configReader.face_cascade_path))

    @exception
    def run_detector(self, image):
        """
        :param image: loaded by cv2.imread
        :return: list of  startX, startY, endX, endY
        """

        imageInGray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
        faces = self.faceCascade.detectMultiScale(imageInGray, scaleFactor=1.2, minNeighbors=1)
        newFaces = [(x, y, x + w, y + h) for x, y, w, h in faces]
        return newFaces
