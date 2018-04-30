import json


class ConfigReader:

    def __init__(self):
        self.configuration = json.load(open("./config.json"))

    @property
    def delta_thresh(self):
        return self.configuration["delta_thresh"]

    @property
    def min_area(self):
        return self.configuration["min_area"]

    @property
    def detected_motion_path(self):
        return self.configuration["detectedMotionPath"]

    @property
    def detected_face_save_path(self):
        return self.configuration["detectedFaceSavePath"]

    @property
    def face_cascade_path(self):
        return self.configuration["face_cascade_path"]

    @property
    def face_recognition_interval(self):
        return self.configuration["time_interval_between_processing_files"]

    @property
    def dnn_model(self):
        return self.configuration["dnn_model"]

    @property
    def proto_txt(self):
        return self.configuration["proto_txt"]

    @property
    def required_face_confidence(self):
        return self.configuration["required_face_confidence"]

    @property
    def training_data(self):
        return self.configuration["training_data"]

    @property
    def openCv_files_path(self):
        return self.configuration["openCv_files_path"]

    @property
    def use_local_db(self):
        return self.configuration["use_local_db"]

    @property
    def linux_driver(self):
        return self.configuration["linux_driver"]

    @property
    def windows_driver(self):
        return self.configuration["windows_driver"]

    @property
    def db_login(self):
        return self.configuration['gear_host_db']["login"]

    @property
    def db_password(self):
        return self.configuration['gear_host_db']["password"]

    @property
    def local_login(self):
        return self.configuration['local_db']["login"]

    @property
    def local_password(self):
        return self.configuration['local_db']["password"]

    @property
    def db_name(self):
        return self.configuration['gear_host_db']["db_name"]

    @property
    def local_db_name(self):
        return self.configuration['local_db']["db_name"]

    @property
    def db_server(self):
        return self.configuration['gear_host_db']["server"]

    @property
    def local_db_server(self):
        return self.configuration['local_db']["server"]
