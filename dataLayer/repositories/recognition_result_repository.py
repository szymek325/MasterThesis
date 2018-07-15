from dataLayer.database_connection import Base, engine, Session
from dataLayer.entities.recognition_result import RecognitionResult


class RecognitionResultRepository():
    def add_recognition_result(self, identity, recognition_id, confidence, file_id):
        Base.metadata.create_all(engine)
        session = Session()
        reading = RecognitionResult(identity, recognition_id, confidence, file_id)
        session.add(reading)
        session.commit()
        session.close()
