import cv2
from sqlalchemy import null

from configuration_global.directory_manager import DirectoryManager
from dataLayer.entities.face_detection import FaceDetection
from dropbox_integration.dropbox_client import DropboxClient
from dataLayer.database_connection import Base, engine, Session
from faceDetection.dnn_face_detector import DnnFaceDetector
from faceDetection.haar_face_detector import HaarFaceDetector
from configuration_global.config_reader import ConfigReader
from configuration_global.exception_handler import exception
from configuration_global.logger_factory import LoggerFactory


class FaceDetectionProcess():
    def __init__(self):
        self.config = ConfigReader()
        self.dbxClient = DropboxClient()
        self.haarDetector = HaarFaceDetector()
        self.dnnDetector = DnnFaceDetector()
        self.logger = LoggerFactory()
        self.requests_path = self.config.face_detection_requests_path
        self.directory = DirectoryManager()

    @exception
    def run_face_detection(self):
        requests = self.__get_all_requests__()
        if not requests == null:
            for request in requests:
                print(request.id)
                self.dbxClient.download_face_detection_input(request.id, self.requests_path)
                image = cv2.imread(f"{self.requests_path}{request.id}/input.jpg")
                faces_detected_by_Haar = self.haarDetector.run_detector(image)
                faces_detected_by_Dnn = self.dnnDetector.run_detector(image)
                self.logger.info(f"Faces detected by "
                                 f"\n   Haar: {faces_detected_by_Haar}"
                                 f"\n   DNN: {faces_detected_by_Dnn}")
                self.__draw_faces__(image, faces_detected_by_Haar, faces_detected_by_Dnn, request.id)
                self.__prepare_to_upload(request.id)
                self.__mark_as_completed__(request)

    @staticmethod
    def __mark_as_completed__(request):
        Base.metadata.create_all(engine)
        session = Session()
        requests = session.query(FaceDetection).filter_by(id=request.id)
        for req in requests:
            req.statusId = 3
        session.commit()
        session.close()

    @staticmethod
    def __get_all_requests__():
        Base.metadata.create_all(engine)
        session = Session()
        requests = session.query(FaceDetection).filter_by(statusId=1)
        session.close()
        return requests

    def __draw_faces__(self, sourceImage, haarFaces, dnnFaces, id):
        haar = sourceImage.copy()
        dnn = sourceImage.copy()
        if len(haarFaces) is not 0:
            for face in haarFaces:
                startX, startY, endX, endY = face
                cv2.rectangle(haar, (startX, startY), (endX, endY), (0, 255, 0), 2)  # green
        if len(dnnFaces) is not 0:
            for face in dnnFaces:
                startX, startY, endX, endY = face
                cv2.rectangle(dnn, (startX, startY), (endX, endY), (0, 0, 255), 2)  # red
        save_path = f"{self.requests_path}{id}"
        self.directory.create_directory_if_doesnt_exist(save_path)
        cv2.imwrite(f"{save_path}/haar.png", haar)
        cv2.imwrite(f"{save_path}/dnn.png", dnn)

    def __prepare_to_upload(self, id):
        path = f"{self.requests_path}{id}"
        haar = open(f"{path}/haar.png", 'rb')
        dnn = open(f"{path}/dnn.png", 'rb')
        self.dbxClient.upload_file(haar.read(), id, "haar.png")
        self.dbxClient.upload_file(dnn.read(), id, "dnn.png")


if __name__ == "__main__":
    drop = FaceDetectionProcess()
    drop.run_face_detection()
