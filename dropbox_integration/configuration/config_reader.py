import json


class ConfigReader:

    def __init__(self):
        self.configuration = json.load(open("configuration/config.json"))

    @property
    def dropbox_access_token(self):
        return self.configuration["dropbox_access_token"]

    @property
    def face_detection_jobs_path(self):
        return self.configuration["face_detection_jobs_path"]
