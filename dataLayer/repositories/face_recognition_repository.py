from configuration_global.exception_handler import exception
from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.face_recognition import FaceRecognition


class FaceRecognitionRepository():

    @exception
    def get_all_not_completed(self):
        Base.metadata.create_all(engine)
        session = Session()
        requests = session.query(FaceRecognition).filter_by(statusId=1)
        session.close()
        return requests

    @exception
    def complete_request(self, request_id):
        Base.metadata.create_all(engine)
        session = Session()
        requests = session.query(FaceRecognition).filter_by(id=request_id)
        for req in requests:
            req.statusId = 3
        session.commit()
        session.close()
