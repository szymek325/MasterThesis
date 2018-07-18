import datetime

from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.recognition import Recognition


class FaceRecognitionRepository():

    def get_all_not_completed(self):
        Base.metadata.create_all(engine)
        session = Session()
        requests = session.query(Recognition).filter_by(statusId=1)
        session.close()
        return requests

    def complete_request(self, request_id):
        Base.metadata.create_all(engine)
        session = Session()
        requests = session.query(Recognition).filter_by(id=request_id)
        for req in requests:
            req.statusId = 3
        session.commit()
        session.close()

    def complete_with_error(self, id):
        Base.metadata.create_all(engine)
        session = Session()
        requests = session.query(Recognition).filter_by(id=id)
        for req in requests:
            req.statusId = 4
            req.completionTime = datetime.datetime.now()
        session.commit()
        session.close()
