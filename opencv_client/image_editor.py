import cv2

from configuration_global.logger_factory import LoggerFactory


class ImageEditor():
    def __init__(self):
        self.logger = LoggerFactory()

    def draw_faces(self, source_image, faces):
        new_image = source_image.copy()
        if len(faces) is not 0:
            for face in faces:
                startX, startY, endX, endY = face
                cv2.rectangle(new_image, (startX, startY), (endX, endY), (0, 255, 0), 2)  # green
        return new_image
