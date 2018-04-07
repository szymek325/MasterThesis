from helpers.config_reader import ConfigReader
from helpers.exception_handler import exception
from helpers.files_manager import FilesManager
from helpers.logger_factory import LoggerFactory
from image_operators.dnn_face_detector import DnnFaceDetector
from PIL import Image
import numpy as np
import cv2


class FaceRecognitionTrainer:
    def __init__(self):
        self.config = ConfigReader()
        self.filesManager = FilesManager()
        self.logger = LoggerFactory()
        self.faceDetector = DnnFaceDetector()
        self.recognizer = cv2.face.LBPHFaceRecognizer()

    @exception
    def train_neural_network(self):
        filePaths = self.filesManager.get_faces_filepaths()
        faceSamples = []
        ids = []
        for imagePath in filePaths:
            image = cv2.imread(imagePath)
            faces = self.faceDetector.run_detector(image)
            id = 1
            if len(faces) is not 0:
                pilImage = Image.open(imagePath).convert('L')
                imageNp = np.array(pilImage, 'uint8')
            for (startX, startY, endX, endY) in faces:
                faceSamples.append(imageNp[startY:endY, startX:endX])
                ids.append(id)
                self.logger.info('adding samle to l;earning string')
        self.recognizer.train(faceSamples, np.array(ids))
        self.recognizer.save('./faceRecognizer.xml')


if __name__ == "__main__":
    faceRecognitionTrainer = FaceRecognitionTrainer()
    faceRecognitionTrainer.train_neural_network()
