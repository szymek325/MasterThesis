import json
import os


class ConfigReader:

    def __init__(self):
        self.dir_path = os.path.dirname(os.path.realpath(__file__))
        self.project_directory = os.path.abspath(os.path.join(self.dir_path, ".."))
        self.configuration = json.load(open(f"{self.dir_path}/config.json"))

    @property
    def detected_motion_path(self):
        return os.path.join(self.project_directory, self.configuration["detectedMotionPath"])

    @property
    def detected_face_save_path(self):
        return os.path.join(self.project_directory, self.configuration["detectedFaceSavePath"])

    @property
    def training_data(self):
        return os.path.join(self.project_directory, self.configuration["training_data"])

    @property
    def logs_path(self):
        return os.path.join(self.project_directory, self.configuration["logs_path"])

    @property
    def local_people_path(self):
        return os.path.join(self.project_directory, self.configuration["local_people_path"])

    @property
    def face_recognition_interval(self):
        return self.configuration["face_recognition_interval"]

    @property
    def people_path(self):
        return self.configuration["people_path"]

    @property
    def face_detection_requests_path(self):
        return os.path.join(self.project_directory,
                            self.configuration["face_detection_requests_path"])
