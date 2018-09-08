from dataLayer.database_connection import Base, engine, Session


class RecognitionResultRepository():
    def add_recognition_result(self, recognition_result):
        Base.metadata.create_all(engine)
        session = Session()
        session.add(recognition_result)
        session.commit()
        session.close()
