import cv2

from cognitive_face_client.azure_face_recognizer import AzureFaceRecognizer
from configuration_global.logger_factory import LoggerFactory
from domain.face_detection.face_detectors_manager import FaceDetectorsManager
from domain.face_recognition.face_recognizer_provider import FaceRecognizerProvider
from domain.people.people_images_provider import PeopleImagesProvider
from testing_runners.OpenCvFaceRecognizerForTesting import OpenCvFaceRecognizerForTesting


class Tester:
    def __init__(self):
        self.logger = LoggerFactory()
        self.imagesProvider = PeopleImagesProvider()
        self.recognizersProvider = FaceRecognizerProvider()
        self.openCvRecognizer = OpenCvFaceRecognizerForTesting()
        self.azureRecognizer = AzureFaceRecognizer()
        self.faceDetector = FaceDetectorsManager()
        self.neuralNetworkToUse = 40

    def test_all_photos(self):
        faces = 0
        all_photos_with_people = self.imagesProvider.get_image_paths_for_people(range(2, 102), 20)
        self.logger.info("test")
        for person_id, person_image in all_photos_with_people:
            found_faces= self.faceDetector.get_face_by_azure(person_image)
            if found_faces == 0 or len(found_faces) == 0:
                continue
            else:
                faces = faces + 1
            self.logger.info(f"FACES: {faces} ")


if __name__ == "__main__":
    program = Tester()
    program.test_all_photos()
