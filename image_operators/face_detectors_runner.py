import cv2
import time
import os
from helpers.config_reader import ConfigReader
from helpers.exception_handler import exception
from helpers.files_manager import FilesManager
from helpers.logger_factory import LoggerFactory
from image_operators.dnn_face_detector import DnnFaceDetector
from image_operators.haar_face_detector import HaarFaceDetector


class FaceDetectorsRunner:
    def __init__(self):
        self.logger = LoggerFactory()
        self.configReader = ConfigReader()
        self.filesManager = FilesManager()
        self.haarDetector = HaarFaceDetector()
        self.dnnDetector = DnnFaceDetector()

    @exception
    def run_face_detector(self):
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
            self.haarDetector.detect_faces(fileName, image)
            self.dnnDetector.detect_faces(fileName, image)
            os.remove(f"{self.configReader.detectedMotionPath}{fileName}")
