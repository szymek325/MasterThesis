import json
import os


class ConfigReader:

    def __init__(self):
        self.dir_path = os.path.dirname(os.path.realpath(__file__))
        self.project_directory = os.path.abspath(os.path.join(self.dir_path, ".."))
        self.configuration = json.load(open(f"{self.dir_path}/config.json"))

    @property
    def logs_path(self):
        return os.path.join(self.project_directory, self.configuration["logs_path"])

    @property
    def local_files_path(self):
        return os.path.join(self.project_directory, self.configuration["local_files_path"])

    @property
    def environment_to_use(self):
        return self.configuration["environment_to_use"]
