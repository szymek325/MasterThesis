import cv2
import time
import os
from helpers.config_reader import ConfigReader
from helpers.exception_handler import exception
from helpers.files_manager import FilesManager
from helpers.logger_factory import LoggerFactory


class FaceDetector():
    def __init__(self):
        self.logger = LoggerFactory()
        self.configReader = ConfigReader()
        self.faceCascade = cv2.CascadeClassifier(self.configReader.face_cascade_path)
        self.filesManager = FilesManager()

    @exception
    def check_for_files_to_process(self):
        while True:
            filesToProcess = self.filesManager.get_unprocessed_files()
            if len(filesToProcess) is 0:
                self.logger.info("No files to process detected, going into sleep")
                time.sleep(self.configReader.face_recognition_interval)
            else:
                self.process_files(filesToProcess)

    def process_files(self, files):
        for fileName in files:
            self.logger.info(f"File: {fileName} loaded for processing")
            image = cv2.imread(f"{self.configReader.detectedMotionPath}{fileName}")
            faces = self.get_faces_from_image(image)
            if len(faces) is not 0:
                for face in faces:
                    x, y, w, h = face
                    cv2.rectangle(image, (x, y), (x + w, y + h), (0, 255, 0), 2)
                self.filesManager.save_face(image, fileName)
            else:
                self.logger.info("No faces detected")
            os.remove(f"{self.configReader.detectedMotionPath}{fileName}")

    def get_faces_from_image(self, image):
        imageInGray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
        faces = self.faceCascade.detectMultiScale(imageInGray, scaleFactor=1.2, minNeighbors=1)
        return faces
