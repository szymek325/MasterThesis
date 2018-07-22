import datetime

from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.detection import Detection


class FaceDetectionRepository():

    def get_all_not_completed(self):
        Base.metadata.create_all(engine)
        session = Session()
        requests = session.query(Detection).filter_by(statusId=1)
        session.close()
        return requests

    def complete_request(self, id, haar_len, dnn_len):
        Base.metadata.create_all(engine)
        session = Session()
        requests = session.query(Detection).filter_by(id=id)
        for req in requests:
            req.statusId = 3
            req.dnnFaces = dnn_len
            req.haarFaces = haar_len
            req.completionTime = datetime.datetime.now()
        session.commit()
        session.close()

    def complete_with_error(self, id):
        Base.metadata.create_all(engine)
        session = Session()
        requests = session.query(Detection).filter_by(id=id)
        for req in requests:
            req.statusId = 4
            req.completionTime = datetime.datetime.now()
        session.commit()
        session.close()
