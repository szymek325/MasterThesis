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
    def detect_faces(self, fileName, image):
        imageToSave = image.copy()
        self.wasSomethingDetected = False
        (h, w) = imageToSave.shape[:2]
        blob = cv2.dnn.blobFromImage(cv2.resize(imageToSave, (300, 300)), 1.0, (300, 300), (104.0, 177.0, 123.0))
        self.net.setInput(blob)
        detections = self.net.forward()
        self.draw_faces_on_source_image(detections, h, imageToSave, w)
        if self.wasSomethingDetected:
            self.filesManager.save_face(imageToSave, fileName.replace(".jpg", "dnn"))

    def draw_faces_on_source_image(self, detections, h, imageToSave, w):
        for i in range(0, detections.shape[2]):
            confidence = detections[0, 0, i, 2]
            if confidence > self.configReader.required_face_confidence:
                box = detections[0, 0, i, 3:7] * numpy.array([w, h, w, h])
                (startX, startY, endX, endY) = box.astype("int")
                text = "{:.2f}%".format(confidence * 100)
                y = startY - 10 if startY - 10 > 10 else startY + 10
                cv2.rectangle(imageToSave, (startX, startY), (endX, endY), (0, 0, 255), 2)
                cv2.putText(imageToSave, text, (startX, y), cv2.FONT_HERSHEY_SIMPLEX, 0.45, (0, 0, 255), 2)
                self.wasSomethingDetected = True
