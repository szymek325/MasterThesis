from cognitive_face_client.cognitive_client import CognitiveClient
from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from opencv_client.face_detection.dnn_face_detector import DnnFaceDetector
from opencv_client.face_detection.haar_face_detector import HaarFaceDetector


class FaceDetectorsManager():
    def __init__(self):
        self.config = ConfigReader()
        self.logger = LoggerFactory()
        self.haarDetector = HaarFaceDetector()
        self.dnnDetector = DnnFaceDetector()
        self.azureClient = CognitiveClient()

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
        faces_detected_by_azure = self.azureClient.async_detect(file_path)
        self.logger.info(f"Faces detected by "
                         f"\n   Azure: {faces_detected_by_azure}")
        return faces_detected_by_azure
