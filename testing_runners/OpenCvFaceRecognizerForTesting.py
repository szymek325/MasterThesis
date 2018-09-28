import cv2
import time

from configuration_global.logger_factory import LoggerFactory
from domain.face_detection.face_detectors_manager import FaceDetectorsManager
from opencv_client.image_converters.image_converter import ImageConverter


class OpenCvFaceRecognizerForTesting():
    def __init__(self):
        self.logger = LoggerFactory()
        self.faceDetectorManager = FaceDetectorsManager()
        self.imageConverter = ImageConverter()

    def recognize_with_single_recognizer_with_haar(self, face_recognizer, image_path):
        detection_start = time.time()
        image = cv2.imread(image_path)
        detected_faces = self.faceDetectorManager.get_face_by_haar(image)
        detection_end = time.time()
        detection_time = detection_end - detection_start
        start_time = time.time()
        self.logger.info(f"Using {face_recognizer} recognizer on {image_path}")
        if len(detected_faces) is 0:
            return 0
        (startX, startY, endX, endY) = detected_faces[0]
        predict_image = self.imageConverter.convert_to_np_array(image[startY:endY, startX:endX])
        nbr_predicted, confidence = face_recognizer.predict(predict_image)
        return nbr_predicted

    def recognize_with_single_recognizer_with_dnn(self, face_recognizer, image_path):
        detection_start = time.time()
        image = cv2.imread(image_path)
        detected_faces = self.faceDetectorManager.get_face_by_dnn(image)
        detection_end = time.time()
        detection_time = detection_end - detection_start
        start_time = time.time()
        self.logger.info(f"Using {face_recognizer} recognizer on {image_path}")
        if len(detected_faces) is 0:
            return 0
        (startX, startY, endX, endY) = detected_faces[0]
        predict_image = self.imageConverter.convert_to_np_array(image[startY:endY, startX:endX])
        nbr_predicted, confidence = face_recognizer.predict(predict_image)
        return nbr_predicted
