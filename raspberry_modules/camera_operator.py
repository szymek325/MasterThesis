import cv2
import time

from configuration_global.logger_factory import LoggerFactory

PI_CAMERA_USED = False
CAMERA_WARMUP_TIME = 2.5
CAMERA_PORT = 0


class CameraOperator():
    def __init__(self):
        self.logger = LoggerFactory()

    def init_camera(self):
        if PI_CAMERA_USED:
            print("Picamera cannot be launch on windows")
            raise Exception("Picamera cannot be launch on windows")
        # COnfiguration for Raspberry
        # camera = PiCamera()
        # camera.resolution = tuple(conf["resolution"])
        # camera.framerate = conf["fps"]
        # rawCapture = PiRGBArray(camera, size=tuple(conf["resolution"]))
        else:
            self.camera = cv2.VideoCapture(CAMERA_PORT)
        time.sleep(CAMERA_WARMUP_TIME)

    def release_camera(self):
        self.camera.release()
        cv2.destroyAllWindows()

    def grab_frame(self):
        grabbed, frame = self.camera.read()
        if not grabbed:
            self.logger.error("Camera didn't grab a frame, raising error")
            raise Exception("Camera didn't grab a frame, raising error")
        return frame

    def show_video(self, frame):
        cv2.imshow("Security Feed", frame)
