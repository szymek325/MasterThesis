import json
import os


class ConfigReader:

    def __init__(self):
        self.dir_path = os.path.dirname(os.path.realpath(__file__))
        self.configuration = json.load(open(f"{self.dir_path}/config.json"))

    @property
    def openCv_files_path(self):
        return self.dir_path
