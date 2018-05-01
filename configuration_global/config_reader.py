import json
import os


class ConfigReader:

    def __init__(self):
        dir_path = os.path.dirname(os.path.realpath(__file__))
        self.configuration = json.load(open(f"{dir_path}/config.json"))

    @property
    def detected_motion_path(self):
        return self.configuration["detectedMotionPath"]

    @property
    def detected_face_save_path(self):
        return self.configuration["detectedFaceSavePath"]

    @property
    def training_data(self):
        return self.configuration["training_data"]

    @property
    def face_recognition_interval(self):
        return self.configuration["face_recognition_interval"]

    @property
    def face_detection_requests_path(self):
        return self.configuration["face_detection_requests_path"]
