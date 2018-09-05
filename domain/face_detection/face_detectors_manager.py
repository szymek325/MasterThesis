import time
import cv2

from cognitive_face_client.clients.azure_face_client import AzureFaceClient
from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from dataLayer.type_providers.detection_types import DetectionTypes
from opencv_client.face_detection.dnn_face_detector import DnnFaceDetector
from opencv_client.face_detection.haar_face_detector import HaarFaceDetector


class FaceDetectorsManager():
    def __init__(self):
        self.config = ConfigReader()
        self.logger = LoggerFactory()
        self.haarDetector = HaarFaceDetector()
        self.dnnDetector = DnnFaceDetector()
        self.azureDetectionClient = AzureFaceClient()
        self.detectionTypes = DetectionTypes()

    def get_faces_on_image_from_file_path(self, file_path):
        image = cv2.imread(file_path)
        time_before = time.time()
        faces_detected_by_haar = self.haarDetector.run_detector(image)
        time_after_haar = time.time()
        faces_detected_by_dnn = self.dnnDetector.run_detector(image)
        time_after_dnn = time.time()
        faces_detected_by_azure = self.azureDetectionClient.get_face_rectangles(file_path)
        time_after_azure = time.time()
        result = [[self.detectionTypes.haar, faces_detected_by_haar, time_after_haar - time_before],
                  [self.detectionTypes.dnn, faces_detected_by_dnn, time_after_dnn - time_after_haar],
                  [self.detectionTypes.azure, faces_detected_by_azure, time_after_azure - time_after_dnn]]
        self.logger.info(f"Faces detected: \n {result}")
        return result

    def get_faces_on_image(self, image):
        faces_detected_by_haar = self.haarDetector.run_detector(image)
        faces_detected_by_dnn = self.dnnDetector.run_detector(image)
        self.logger.info(f"Faces detected by "
                         f"\n   Haar: {faces_detected_by_haar}"
                         f"\n   DNN: {faces_detected_by_dnn}")
        return faces_detected_by_haar, faces_detected_by_dnn

    def get_face_by_haar(self, image):
        faces_detected_by_haar = self.haarDetector.run_detector(image)
        self.logger.info(f"Faces detected by "
                         f"\n   Haar: {faces_detected_by_haar}")
        return faces_detected_by_haar

    def get_face_by_dnn(self, image):
        faces_detected_by_dnn = self.dnnDetector.run_detector(image)
        self.logger.info(f"Faces detected by "
                         f"\n   DNN: {faces_detected_by_dnn}")
        return faces_detected_by_dnn

    def get_face_by_azure(self, file_path):
        faces_detected_by_azure = self.azureDetectionClient.get_face_rectangles(file_path)
        self.logger.info(f"Faces detected by "
                         f"\n   Azure: {faces_detected_by_azure}")
        return faces_detected_by_azure
