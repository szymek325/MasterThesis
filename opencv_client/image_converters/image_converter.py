from configuration_global.logger_factory import LoggerFactory
from PIL import Image
import cv2
import numpy as np


class ImageConverter():
    def __init__(self):
        self.logger = LoggerFactory()

    def convert_to_np_array(self, image):
        """

        :rtype: numpy array
        """
        width_d, height_d = 280, 280  # Declare your own width and height
        face = cv2.resize(image, (width_d, height_d))
        pil_image = cv2.cvtColor(face, cv2.COLOR_BGR2RGB)
        pil_image = Image.fromarray(pil_image).convert('L')
        numpy_array = np.array(pil_image, 'uint8')
        return numpy_array
