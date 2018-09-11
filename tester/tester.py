import cv2

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
        self.neuralNetworkToUse = 18

    def test_all_photos(self):
        azure_counter = 0
        fisher_counter = 0
        eigen_counter = 0
        lbph_counter = 0
        all_photos_with_people = self.imagesProvider.get_image_paths_for_people(range(2, 102), 20)
        fisher, eigen, lbph = self.recognizersProvider.create_open_cv_face_recognizers_with_type(
            self.neuralNetworkToUse)
        self.logger.info("test")
        for person_id, person_image in all_photos_with_people:
            azure_result = 0
            fisher_result = 0
            eigen_result = 0
            lbph_result = 0
            try:
                azure_result = self.azureRecognizer.recognize_face_without_db(self.neuralNetworkToUse, person_image)
                fisher_result = self.openCvRecognizer.recognize_with_single_recognizer(fisher, person_image)
                eigen_result = self.openCvRecognizer.recognize_with_single_recognizer(eigen, person_image)
                lbph_result = self.openCvRecognizer.recognize_with_single_recognizer(lbph, person_image)
            except Exception as exception:
                self.logger.error(exception)
            if person_id == int(azure_result):
                azure_counter = azure_counter + 1
            if person_id == fisher_result:
                fisher_counter = fisher_counter + 1
            if person_id == eigen_result:
                eigen_counter = eigen_counter + 1
            if person_id == lbph_result:
                lbph_counter = lbph_counter + 1
            self.logger.info(
                f"Azure: {azure_counter} Fisher: {fisher_counter} Eigen: {eigen_counter} LBPH: {lbph_counter}")


if __name__ == "__main__":
    program = Tester()
    program.test_all_photos()
