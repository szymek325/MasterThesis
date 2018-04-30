import json


class ConfigReader:

    def __init__(self):
        self.configuration = json.load(open("faceDetection/configuration/config.json"))

    @property
    def face_cascade_path(self):
        return self.configuration["face_cascade_path"]

    @property
    def dnn_model(self):
        return self.configuration["dnn_model"]

    @property
    def proto_txt(self):
        return self.configuration["proto_txt"]

    @property
    def required_face_confidence(self):
        return self.configuration["required_face_confidence"]
