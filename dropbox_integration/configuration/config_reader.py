import json
import os


class ConfigReader:

    def __init__(self):
        dir_path = os.path.dirname(os.path.realpath(__file__))
        self.configuration = json.load(open(f"{dir_path}/config.json"))

    @property
    def dropbox_access_token(self):
        return self.configuration["dropbox_access_token"]

    @property
    def face_detection_jobs_path(self):
        return self.configuration["face_detection_jobs_path"]
