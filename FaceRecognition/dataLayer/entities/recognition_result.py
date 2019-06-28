from sqlalchemy import Column, String, Integer, Date, ForeignKey, Float

from dataLayer.database_connection import Base


class RecognitionResult(Base):
    __tablename__ = 'RecognitionResult'

    id = Column(Integer, primary_key=True)
    identified_person_id = Column('IdentifiedPersonId', Integer)
    confidence = Column('Confidence', Integer)
    neural_network_file_id = Column("NeuralNetworkFileId", Integer, ForeignKey("NeuralNetworkFile.Id"))
    recognition_id = Column("RecognitionId", Float, ForeignKey("Recognition.Id"))
    comments = Column('Comments', String)
    processing_time = Column('ProcessingTime', String)

    def __init__(self, identity, recognition_id, confidence, file_id, proc_time="", comments=""):
        self.identified_person_id = identity
        self.recognition_id = recognition_id
        self.confidence = confidence
        self.neural_network_file_id = file_id
        self.comments = comments
        self.processing_time = proc_time
