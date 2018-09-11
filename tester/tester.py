from cognitive_face_client.azure_face_recognizer import AzureFaceRecognizer
from configuration_global.logger_factory import LoggerFactory
from domain.face_recognition.face_recognizer_provider import FaceRecognizerProvider
from domain.people.people_images_provider import PeopleImagesProvider
from opencv_client.face_recognition.open_cv_face_recognizer import OpenCvFaceRecognizer


class Tester:
    def __init__(self):
        self.logger = LoggerFactory()
        self.imagesProvider = PeopleImagesProvider()
        self.recognizersProvider = FaceRecognizerProvider()
        self.openCvRecognizer = OpenCvFaceRecognizer()
        self.azureRecognizer = AzureFaceRecognizer()

    def test_all_photos(self):
        all_photos_with_people = self.imagesProvider.get_image_paths_for_people(range(2, 102), 20)
        recognizers = self.recognizersProvider.create_open_cv_face_recognizers_for_request("nn_id")
        self.logger.info("test")


if __name__ == "__main__":
    program = Tester()
    program.test_all_photos()
