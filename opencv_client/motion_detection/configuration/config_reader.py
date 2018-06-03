import json
import os


class ConfigReader:

    def __init__(self):
        dir_path = os.path.dirname(os.path.realpath(__file__))
        self.configuration = json.load(open(f"{dir_path}/config.json"))

    @property
    def delta_thresh(self):
        return self.configuration["delta_thresh"]

    @property
    def min_area(self):
        return self.configuration["min_area"]

    @property
    def movement_timestamp(self):
        return self.configuration["movement_timestamp"]

    @property
    def movement_marking(self):
        return self.configuration["movement_marking"]

    @property
    def show_video(self):
        return self.configuration["show_video"]

    @property
    def pi_camera_used(self):
        return self.configuration["piCameraUsed"]

    @property
    def camera_port(self):
        return self.configuration["cameraPort"]

    @property
    def camera_warmup_time(self):
        return self.configuration["camera_warmup_time"]

    @property
    def min_motion_frames(self):
        return self.configuration["min_motion_frames"]

    @property
    def resolution(self):
        return self.configuration["resolution"]

    @property
    def fps(self):
        return self.configuration["fps"]
