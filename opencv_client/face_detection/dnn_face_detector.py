import cv2
import numpy

from configuration_global.exception_handler import exception
import os

from opencv_client.face_detection.configuration.config_reader import ConfigReader


class DnnFaceDetector:
    def __init__(self):
        self.configReader = ConfigReader()
        self.net = cv2.dnn.readNetFromCaffe(os.path.abspath(self.configReader.proto_txt),
                                            os.path.abspath(self.configReader.dnn_model))

    @exception
    def run_detector(self, image):
        """
        :param image: loaded by cv2.imread
        :return:list of  startX, startY, endX, endY
        """
        (h, w) = image.shape[:2]
        blob = cv2.dnn.blobFromImage(cv2.resize(image, (300, 300)), 1.0, (300, 300), (104.0, 177.0, 123.0))
        self.net.setInput(blob)
        detections = self.net.forward()
        return self.__detect_faces__(detections, h, w)

    def __detect_faces__(self, detections, h, w):
        faces = []
        for i in range(0, detections.shape[2]):
            confidence = detections[0, 0, i, 2]
            if confidence > self.configReader.required_face_confidence:
                box = detections[0, 0, i, 3:7] * numpy.array([w, h, w, h])
                faces.append(box.astype("int"))
        return faces
