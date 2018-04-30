from faceRecognition.face_recognizer import FaceRecognizer
from helpers.exception_handler import exception
from helpers.files_manager import FilesManager
from helpers.logger_factory import LoggerFactory
from PIL import Image
import numpy as np
import cv2
import os



class FaceRecognitionRunner:
    def __init__(self):
        self.filesManager = FilesManager()
        self.logger = LoggerFactory()
        self.faceRecognizer = FaceRecognizer()

    @exception
    def run_face_Recognition(self):
        filePaths = self.filesManager.get_training_data()
        for imagePath in filePaths:
            pilImage = Image.open(imagePath).convert("RGB")
            opencvImage = cv2.cvtColor(np.array(pilImage), cv2.COLOR_RGB2BGR)
            faceId = int(os.path.split(imagePath)[1].split("_")[0].replace("subject", "").replace(".jpg", ""))
            self.faceRecognizer.recognize_face_and_print_result(opencvImage, faceId)


if __name__ == "__main__":
    faceRecognitionRunner = FaceRecognitionRunner()
    faceRecognitionRunner.run_face_Recognition()
