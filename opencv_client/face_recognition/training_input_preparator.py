import cv2

from opencv_client.face_detection.haar_face_detector import HaarFaceDetector
from opencv_client.image_converters.image_converter import ImageConverter


class TrainingDataExtractor():
    def __init__(self):
        self.faceDetector = HaarFaceDetector()
        self.imageConverter = ImageConverter()

    def extract_training_data_from_images(self, images_paths, person_id):
        face_samples = []
        ids = []
        for imagePath in images_paths:
            open_cv_image = cv2.imread(imagePath)
            faces = self.faceDetector.run_detector(open_cv_image)
            if len(faces) is not 0:
                (startX, startY, endX, endY) = faces[0]
                np_image = self.imageConverter.convert_to_np_array(open_cv_image[startY:endY, startX:endX])
                face_samples.append(np_image)
                ids.append(person_id)
        return face_samples, ids
