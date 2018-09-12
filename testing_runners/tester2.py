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
        self.neuralNetworkToUse = 41

    def test_all_photos(self):
        azure_counter = 0
        fisher_counter = 0
        eigen_counter = 0
        lbph_counter = 0
        faces = 0
        all_photos_with_people = self.imagesProvider.get_image_paths_for_people(range(2, 102), 20)
        fisher, eigen, lbph = self.recognizersProvider.create_open_cv_face_recognizers_with_type(
            self.neuralNetworkToUse)
        self.logger.info("test")
        for person_id, person_image in all_photos_with_people:
            found_faces = self.faceDetector.get_face_by_dnn_with_load_image(person_image)
            if found_faces == 0 or len(found_faces) == 0:
                continue
            else:
                faces = faces + 1
            azure_result = 0
            fisher_result = 0
            eigen_result = 0
            lbph_result = 0
            try:
                azure_result = self.azureRecognizer.recognize_face_without_db(31, person_image)
                fisher_result = self.openCvRecognizer.recognize_with_single_recognizer_with_dnn(fisher, person_image)
                eigen_result = self.openCvRecognizer.recognize_with_single_recognizer_with_dnn(eigen, person_image)
                lbph_result = self.openCvRecognizer.recognize_with_single_recognizer_with_dnn(lbph, person_image)
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
            self.logger.info(f"FACES: {faces} Azure: {azure_counter} Fisher: {fisher_counter}"
                             f" Eigen: {eigen_counter} LBPH: {lbph_counter}")


if __name__ == "__main__":
    program = Tester()
    program.test_all_photos()
