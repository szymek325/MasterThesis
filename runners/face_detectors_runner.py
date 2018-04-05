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
    def run_face_detector_on_motion_detector_pictures(self):
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
            faces_detected_by_Haar = self.haarDetector.run_detector(image)
            faces_detected_by_Dnn = self.dnnDetector.run_detector(image)
            self.logger.info(f"Faces detected by "
                             f"\n   Haar: {faces_detected_by_Haar}"
                             f"\n   DNN: {faces_detected_by_Dnn}")
            newImage = self.draw_faces(image, faces_detected_by_Haar, faces_detected_by_Dnn)
            naming = fileName.split('_')
            number = naming[2].split('.')[0]
            self.filesManager.save_face(newImage, f"faces_{naming[1]}_{number}")
            os.remove(f"{self.configReader.detectedMotionPath}{fileName}")

    def draw_faces(self, sourceImage, haarFaces, dnnFaces):
        if len(haarFaces) is not 0:
            for face in haarFaces:
                x, y, w, h = face
                cv2.rectangle(sourceImage, (x, y), (x + w, y + h), (0, 255, 0), 2)  # green
        if len(dnnFaces) is not 0:
            for face in dnnFaces:
                startX, startY, endX, endY = face
                cv2.rectangle(sourceImage, (startX, startY), (endX, endY), (0, 0, 255), 2)  # red
        return sourceImage
