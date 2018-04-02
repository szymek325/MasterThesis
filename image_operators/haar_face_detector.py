import cv2
from helpers.config_reader import ConfigReader
from helpers.exception_handler import exception
from helpers.files_manager import FilesManager


class HaarFaceDetector:

    def __init__(self):
        self.configReader = ConfigReader()
        self.filesManager = FilesManager()
        self.faceCascade = cv2.CascadeClassifier(self.configReader.face_cascade_path)

    @exception
    def detect_faces(self, fileName, image):
        imageToSave = image.copy()
        faces = self.get_faces_from_source_image(image)
        if len(faces) is not 0:
            for face in faces:
                x, y, w, h = face
                cv2.rectangle(imageToSave, (x, y), (x + w, y + h), (0, 255, 0), 2)
            self.filesManager.save_face(imageToSave, fileName.replace(".jpg", "_haar"))

    def get_faces_from_source_image(self, image):
        imageInGray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
        faces = self.faceCascade.detectMultiScale(imageInGray, scaleFactor=1.2, minNeighbors=1)
        return faces
