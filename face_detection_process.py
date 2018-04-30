import cv2
from sqlalchemy import null
from dataLayer.entities.face_detection import FaceDetection
from dropbox_integration.dropbox_client import DropboxClient
from dataLayer.database_connection import Base, engine, Session
from faceDetection.dnn_face_detector import DnnFaceDetector
from faceDetection.haar_face_detector import HaarFaceDetector
from helpers.config_reader import ConfigReader
from helpers.exception_handler import exception
from helpers.logger_factory import LoggerFactory


class FaceDetectionProcess():
    def __init__(self):
        self.config = ConfigReader()
        self.dbxClient = DropboxClient()
        self.haarDetector = HaarFaceDetector()
        self.dnnDetector = DnnFaceDetector()
        self.logger = LoggerFactory()
        self.requests_path = self.config.face_detection_requests_path

    @exception
    def run_face_detection(self):
        requests = self.get_all_requests()
        if not requests == null:
            for request in requests:
                print(request.id)
                self.dbxClient.download_face_detection_input(request.id,self.requests_path)
                image = cv2.imread(f"temp/faceDetectionRequests/{request.id}/input.jpg")
                faces_detected_by_Haar = self.haarDetector.run_detector(image)
                faces_detected_by_Dnn = self.dnnDetector.run_detector(image)
                self.logger.info(f"Faces detected by "
                                 f"\n   Haar: {faces_detected_by_Haar}"
                                 f"\n   DNN: {faces_detected_by_Dnn}")

    def get_all_requests(self):
        Base.metadata.create_all(engine)
        session = Session()
        requests = session.query(FaceDetection).filter_by(statusId=1)
        session.close()
        return requests


if __name__ == "__main__":
    drop = FaceDetectionProcess()
    drop.run_face_detection()
