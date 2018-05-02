from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.face_detection import FaceDetection


class FaceDetectionRepository():

    def get_all_not_completed(self):
        Base.metadata.create_all(engine)
        session = Session()
        requests = session.query(FaceDetection).filter_by(statusId=1)
        session.close()
        return requests

    def complete_request(self, id, haar_len, dnn_len):
        Base.metadata.create_all(engine)
        session = Session()
        requests = session.query(FaceDetection).filter_by(id=id)
        for req in requests:
            req.statusId = 3
            req.dnnFaces = dnn_len
            req.haarFaces = haar_len
        session.commit()
        session.close()
