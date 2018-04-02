import cv2
import time
import imutils
import datetime

from helpers.config_reader import ConfigReader
from helpers.files_manager import FilesManager
from helpers.logger_factory import LoggerFactory


class Runner:

    def __init__(self):
        self.configReader = ConfigReader()
        self.logger = LoggerFactory()
        self.filesManager = FilesManager()
        self.camera = 'camera'
        self.averageFrame = None
        self.motionCounter = 0
        self.lastUploaded = datetime.datetime.now()

    def init_camera(self):
        if self.configReader.piCameraUsed:
            print("Picamera cannot be launch on windows")
            raise Exception("Picamera cannot be launch on windows")
        # COnfiguration for Raspberry
        # camera = PiCamera()
        # camera.resolution = tuple(conf["resolution"])
        # camera.framerate = conf["fps"]
        # rawCapture = PiRGBArray(camera, size=tuple(conf["resolution"]))
        else:
            self.camera = cv2.VideoCapture(self.configReader.cameraPort)
        time.sleep(self.configReader.camera_warmup_time)

    def detect_motion(self):
        while True:
            text = "Unoccupied"
            grabbed, frame = self.camera.read()
            if not grabbed:
                self.logger.error("Camera didn't grab a frame, raising error")
                break
            timestamp = datetime.datetime.now()
            frame = imutils.resize(frame, width=500)
            gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
            gray = cv2.GaussianBlur(gray, (21, 21), 0)
            if self.averageFrame is None:
                self.averageFrame = gray.copy().astype("float")
                continue
            cv2.accumulateWeighted(gray, self.averageFrame, 0.5)
            frameDelta = cv2.absdiff(gray, cv2.convertScaleAbs(self.averageFrame))
            thresh = cv2.threshold(frameDelta, self.configReader.delta_thresh, 255, cv2.THRESH_BINARY)[1]
            thresh = cv2.dilate(thresh, None, iterations=2)
            (_, contours, _) = cv2.findContours(thresh.copy(), cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
            for c in contours:
                # if the contour is too small, ignore it
                if cv2.contourArea(c) < self.configReader.min_area:
                    continue
                (x, y, w, h) = cv2.boundingRect(c)
                cv2.rectangle(frame, (x, y), (x + w, y + h), (0, 255, 0), 2)
                text = "Occupied"
                print("Occupied")
            ts = timestamp.strftime("%A %d %B %Y %I:%M:%S%p")
            cv2.putText(frame, "Room Status: {}".format(text), (10, 20), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 0, 255), 2)
            cv2.putText(frame, ts, (10, frame.shape[0] - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.35, (0, 0, 255), 1)
            if text == "Occupied":
                if (timestamp - self.lastUploaded).seconds >= 2.5:
                    motionCounter += 1
                    if motionCounter >= self.configReader.min_motion_frames:
                        self.filesManager.save_motion(frame)
                        print("MOVEMENT SAVED")
            else:
                motionCounter = 0

            if self.configReader.show_video:
                # display the security feed
                cv2.imshow("Security Feed", frame)
                key = cv2.waitKey(1) & 0xFF
                if key == ord("q"):
                    break
        self.camera.release()
        cv2.destroyAllWindows()


runner = Runner()
runner.init_camera()
runner.detect_motion()
