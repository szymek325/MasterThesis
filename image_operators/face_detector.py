import cv2
import time
import os
import numpy
from helpers.config_reader import ConfigReader
from helpers.exception_handler import exception
from helpers.files_manager import FilesManager
from helpers.logger_factory import LoggerFactory


class FaceDetector:
    def __init__(self):
        self.logger = LoggerFactory()
        self.configReader = ConfigReader()
        self.filesManager = FilesManager()
        self.faceCascade = cv2.CascadeClassifier(self.configReader.face_cascade_path)
        self.net = cv2.dnn.readNetFromCaffe(self.configReader.proto_txt, self.configReader.dnn_model)

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
            self.detect_faces_by_haar_method(fileName, image)
            self.detect_faces_by_dnn(fileName, image)
            os.remove(f"{self.configReader.detectedMotionPath}{fileName}")

    def detect_faces_by_dnn(self, fileName, image):
        wasSomethingDetected = False
        (h, w) = image.shape[:2]
        blob = cv2.dnn.blobFromImage(cv2.resize(image, (300, 300)), 1.0, (300, 300), (104.0, 177.0, 123.0))
        self.net.setInput(blob)
        detections = self.net.forward()
        for i in range(0, detections.shape[2]):
            confidence = detections[0, 0, i, 2]
            if confidence > self.configReader.required_face_confidence:
                box = detections[0, 0, i, 3:7] * numpy.array([w, h, w, h])
                (startX, startY, endX, endY) = box.astype("int")
                text = "{:.2f}%".format(confidence * 100)
                y = startY - 10 if startY - 10 > 10 else startY + 10
                cv2.rectangle(image, (startX, startY), (endX, endY), (0, 0, 255), 2)
                cv2.putText(image, text, (startX, y), cv2.FONT_HERSHEY_SIMPLEX, 0.45, (0, 0, 255), 2)
                wasSomethingDetected = True
        if wasSomethingDetected:
            fileName = fileName.replace("movement", "dnn")
            self.filesManager.save_face(image, fileName)

    def detect_faces_by_haar_method(self, fileName, image):
        faces = self.get_faces_from_image_by_haar(image)
        if len(faces) is not 0:
            for face in faces:
                x, y, w, h = face
                cv2.rectangle(image, (x, y), (x + w, y + h), (0, 255, 0), 2)
            self.filesManager.save_face(image, fileName)
        else:
            self.logger.info("No faces detected")

    def get_faces_from_image_by_haar(self, image):
        imageInGray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
        faces = self.faceCascade.detectMultiScale(imageInGray, scaleFactor=1.2, minNeighbors=1)
        return faces
