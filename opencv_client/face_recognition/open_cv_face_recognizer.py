import time

import cv2
from configuration_global.logger_factory import LoggerFactory
from dataLayer.entities.recognition_result import RecognitionResult
from dataLayer.repositories.recognition_result_repository import RecognitionResultRepository
from domain.face_detection.face_detectors_manager import FaceDetectorsManager
from opencv_client.image_converters.image_converter import ImageConverter


class OpenCvFaceRecognizer():
    def __init__(self):
        self.logger = LoggerFactory()
        self.faceDetectorManager = FaceDetectorsManager()
        self.imageConverter = ImageConverter()
        self.recognitionResultRepo = RecognitionResultRepository()

    def recognize_face_from_image(self, request_id, recognizers, image_path):
        start_time = time.time()
        image = cv2.imread(image_path)
        detected_faces = self.faceDetectorManager.get_face_by_haar(image)
        for face_recognizer, file_id in recognizers:
            self.logger.info(f"Using {face_recognizer} recognizer created from {file_id} file id")
            if len(detected_faces) is 0:
                self.__add_empty_result__(file_id, request_id, start_time)
            for (startX, startY, endX, endY) in detected_faces:
                predict_image = self.imageConverter.convert_to_np_array(image[startY:endY, startX:endX])
                nbr_predicted, confidence = face_recognizer.predict(predict_image)
                self.__add_result__(confidence, file_id, nbr_predicted, request_id, start_time)

    def __add_result__(self, confidence, file_id, nbr_predicted, request_id, start_time):
        self.logger.info(f"Recognized identity: {nbr_predicted} confidence:{confidence}")
        end_time = time.time()
        process_time = end_time - start_time
        result = RecognitionResult(nbr_predicted, request_id, confidence, file_id, str(process_time))
        self.recognitionResultRepo.add_recognition_result(result)

    def __add_empty_result__(self, azure_file, request_id, start_time):
        end_time = time.time()
        process_time = end_time - start_time
        result = RecognitionResult(0, request_id, 0, azure_file.id, str(process_time), "No faces detected")
        self.recognitionResultRepo.add_recognition_result(result)
