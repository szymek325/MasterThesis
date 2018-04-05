import cv2
from helpers.config_reader import ConfigReader
from helpers.exception_handler import exception
from helpers.files_manager import FilesManager


class HaarFaceDetector:

    def __init__(self):
        self.configReader = ConfigReader()
        self.filesManager = FilesManager()
        self.faceCascade = cv2.CascadeClassifier(self.configReader.face_cascade_path)

    @exception
    def run_detector(self, image):
        imageInGray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
        faces = self.faceCascade.detectMultiScale(imageInGray, scaleFactor=1.2, minNeighbors=1)
        return faces
