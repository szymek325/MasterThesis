import cv2
import numpy
from helpers.config_reader import ConfigReader
from helpers.exception_handler import exception
from helpers.files_manager import FilesManager


class DnnFaceDetector:
    def __init__(self):
        self.configReader = ConfigReader()
        self.filesManager = FilesManager()
        self.net = cv2.dnn.readNetFromCaffe(self.configReader.proto_txt, self.configReader.dnn_model)
        self.wasSomethingDetected = False

    @exception
    def run_detector(self, image):
        self.wasSomethingDetected = False
        (h, w) = image.shape[:2]
        blob = cv2.dnn.blobFromImage(cv2.resize(image, (300, 300)), 1.0, (300, 300), (104.0, 177.0, 123.0))
        self.net.setInput(blob)
        detections = self.net.forward()
        detectedFaces=self.detect_faces(detections, h, image, w)
        return detectedFaces

    def detect_faces(self, detections, h, imageToSave, w):
        faces=[]
        for i in range(0, detections.shape[2]):
            confidence = detections[0, 0, i, 2]
            if confidence > self.configReader.required_face_confidence:
                box = detections[0, 0, i, 3:7] * numpy.array([w, h, w, h])
                faces.append(box.astype("int"))
        return faces