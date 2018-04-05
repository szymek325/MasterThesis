import time
import cv2
import imutils
from datetime import datetime
from helpers.config_reader import ConfigReader
from helpers.exception_handler import exception
from helpers.files_manager import FilesManager
from helpers.logger_factory import LoggerFactory


class MovementDetector:

    def __init__(self):
        self.logger = LoggerFactory()
        self.filesManager = FilesManager()
        self.configReader = ConfigReader()
        self.averageFrame = None
        self.motionCounter = 0
        self.lastUploaded = datetime.now()
        self.text = "Unoccupied"

    @exception
    def detect_motion(self):
        self.__init_camera__()
        while True:
            self.text = "Unoccupied"
            grabbed, frame = self.camera.read()

            if not grabbed:
                self.logger.error("Camera didn't grab a frame, raising error")
                break

            frame = imutils.resize(frame, width=500)
            gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
            gray = cv2.GaussianBlur(gray, (21, 21), 0)

            if self.averageFrame is None:
                self.averageFrame = gray.copy().astype("float")
                continue

            self.__check_for_movement__(gray, frame)
            timestamp = datetime.now()
            self.__mark_frame__(frame, timestamp)
            self.__save_frame_if_room_is_occupied__(frame, timestamp)

            if self.configReader.show_video:
                cv2.imshow("Security Feed", frame)
                key = cv2.waitKey(1) & 0xFF
                if key == ord("q"):
                    break

        self.camera.release()
        cv2.destroyAllWindows()

    @exception
    def __init_camera__(self):
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

    def __check_for_movement__(self, gray, frame):
        cv2.accumulateWeighted(gray, self.averageFrame, 0.5)
        frameDelta = cv2.absdiff(gray, cv2.convertScaleAbs(self.averageFrame))
        thresh = cv2.threshold(frameDelta, self.configReader.delta_thresh, 255, cv2.THRESH_BINARY)[1]
        thresh = cv2.dilate(thresh, None, iterations=2)
        (_, contours, _) = cv2.findContours(thresh.copy(), cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
        self.__check_contour_size__(contours, frame)

    def __save_frame_if_room_is_occupied__(self, frame, timestamp):
        if self.text == "Occupied":
            if (timestamp - self.lastUploaded).seconds >= 2.5:
                self.motionCounter += 1
                if self.motionCounter >= self.configReader.min_motion_frames:
                    self.filesManager.save_motion(frame)
        else:
            self.motionCounter = 0

    def __mark_frame__(self, frame, timestamp):
        ts = timestamp.strftime("%A %d %B %Y %I:%M:%S%p")
        # cv2.putText(frame, f"Room Status: {self.text}", (10, 20), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 0, 255), 2)
        cv2.putText(frame, ts, (10, frame.shape[0] - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.35, (0, 0, 255), 1)

    def __check_contour_size__(self, contours, frame):
        for c in contours:
            if cv2.contourArea(c) >= self.configReader.min_area:
                # self.draw_contour(c, frame)
                self.text = "Occupied"

    @staticmethod
    def __draw_contour__(c, frame):
        (x, y, w, h) = cv2.boundingRect(c)
        cv2.rectangle(frame, (x, y), (x + w, y + h), (0, 255, 0), 2)
