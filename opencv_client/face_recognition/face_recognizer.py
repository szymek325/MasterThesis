import cv2
from configuration_global.logger_factory import LoggerFactory
from dataLayer.repositories.recognition_result_repository import RecognitionResultRepository
from domain.face_detection.face_detectors_manager import FaceDetectorsManager
from opencv_client.image_converters.image_converter import ImageConverter


class FaceRecognizer():
    def __init__(self):
        self.logger = LoggerFactory()
        self.faceDetectorManager = FaceDetectorsManager()
        self.imageConverter = ImageConverter()
        self.recognitionResultRepo = RecognitionResultRepository()

    def recognize_face_from_image(self, request_id, recognizers, image_path):
        image = cv2.imread(image_path)
        detected_faces = self.faceDetectorManager.get_face_by_haar(image)
        for face_recognizer, type_id in recognizers:
            self.logger.info(f"Using {face_recognizer} recognizer of type {type_id} id")
            for (startX, startY, endX, endY) in detected_faces:
                predict_image = self.imageConverter.convert_to_np_array(image[startY:endY, startX:endX])
                nbr_predicted, confidence = face_recognizer.predict(predict_image)
                self.recognitionResultRepo.add_recognition_result(nbr_predicted, request_id, confidence, type_id)
