from helpers.config_reader import ConfigReader
from helpers.exception_handler import exception
from helpers.files_manager import FilesManager
from helpers.logger_factory import LoggerFactory
from image_operators.dnn_face_detector import DnnFaceDetector
from PIL import Image
import numpy as np
import cv2
import os


class FaceRecognitionTrainer:
    def __init__(self):
        self.config = ConfigReader()
        self.filesManager = FilesManager()
        self.logger = LoggerFactory()
        self.faceDetector = DnnFaceDetector()
        self.recognizer = cv2.face.LBPHFaceRecognizer_create()

    @exception
    def train_neural_network(self):
        filePaths = self.filesManager.get_training_data()
        faceSamples = []
        ids = []
        for imagePath in filePaths:
            pilImage = Image.open(imagePath).convert("RGB")
            opencvImage = cv2.cvtColor(np.array(pilImage), cv2.COLOR_RGB2BGR)
            faces = self.faceDetector.run_detector(opencvImage)
            faceId=int(os.path.split(imagePath)[1].split("_")[0].replace("subject", "").replace(".jpg",""))
            print(f"{faceId}")
            if len(faces) is not 0:
                imageNp = np.array(pilImage.convert('L'), 'uint8')
                for (startX, startY, endX, endY) in faces:
                    faceSamples.append(imageNp[startY:endY, startX:endX])
                    ids.append(faceId)
                    self.logger.info('adding sample to learning array')
        self.recognizer.train(faceSamples, np.array(ids))
        self.recognizer.write(f'{self.config.openCv_files_path}faceRecognizer.yml')



if __name__ == "__main__":
    faceRecognitionTrainer = FaceRecognitionTrainer()
    faceRecognitionTrainer.train_neural_network()
