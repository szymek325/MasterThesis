import json


class ConfigReader:

    def __init__(self):
        self.configuration = json.load(open("./config.json"))

    @property
    def show_video(self):
        return self.configuration["show_video"]

    @property
    def min_motion_frames(self):
        return self.configuration["min_motion_frames"]

    @property
    def camera_warmup_time(self):
        return self.configuration["camera_warmup_time"]

    @property
    def delta_thresh(self):
        return self.configuration["delta_thresh"]

    @property
    def resolution(self):
        return self.configuration["resolution"]

    @property
    def fps(self):
        return self.configuration["fps"]

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
    def pi_camera_used(self):
        return self.configuration["piCameraUsed"]

    @property
    def camera_port(self):
        return self.configuration["cameraPort"]

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
    def movement_timestamp(self):
        return self.configuration["movement_timestamp"]

    @property
    def movement_marking(self):
        return self.configuration["movement_marking"]

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
        return self.configuration["db_login"]

    @property
    def db_password(self):
        return self.configuration["db_password"]

    @property
    def local_login(self):
        return self.configuration["local_login"]

    @property
    def local_password(self):
        return self.configuration["local_password"]

    @property
    def db_name(self):
        return self.configuration["db_name"]

    @property
    def local_db_name(self):
        return self.configuration["local_db_name"]

    @property
    def db_server(self):
        return self.configuration["db_server"]

    @property
    def local_db_server(self):
        return self.configuration["local_db_server"]
