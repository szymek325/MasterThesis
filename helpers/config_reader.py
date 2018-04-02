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
    def detectedMotionPath(self):
        return self.configuration["detectedMotionPath"]

    @property
    def detectedFaceSavePath(self):
        return self.configuration["detectedFaceSavePath"]

    @property
    def piCameraUsed(self):
        return self.configuration["piCameraUsed"]

    @property
    def cameraPort(self):
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
