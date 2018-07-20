from dataLayer.database_connection import Base, engine, Session


class DetectionResultRepository():

    def add_detection_result_with_image(self, detection_result):
        Base.metadata.create_all(engine)
        session = Session()
        session.add(detection_result)
        session.commit()
        generated_id=detection_result.id
        session.close()
        return generated_id
