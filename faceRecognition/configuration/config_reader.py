import json


class ConfigReader:

    def __init__(self):
        self.configuration = json.load(open("faceRecognition/configuration/config.json"))

    @property
    def openCv_files_path(self):
        return self.configuration["openCv_files_path"]
