import json
import os


class OpenCvConfigurationReader:

    def __init__(self):
        self.dir_path = os.path.dirname(os.path.realpath(__file__))
        self.module_directory = os.path.abspath(os.path.join(self.dir_path, ".."))
        self.configuration = json.load(open(f"{self.dir_path}/opencv_configuration.json"))

    @property
    def face_cascade_path(self):
        return os.path.join(self.module_directory, self.configuration["face_cascade_path"])

    @property
    def dnn_model(self):
        return os.path.join(self.module_directory, self.configuration["dnn_model"])

    @property
    def proto_txt(self):
        return os.path.join(self.module_directory, self.configuration["proto_txt"])

    @property
    def required_face_confidence(self):
        return self.configuration["required_face_confidence"]
