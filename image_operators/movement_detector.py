import cv2
import imutils
from datetime import datetime
from helpers.config_reader import ConfigReader
from helpers.exception_handler import exception
from helpers.logger_factory import LoggerFactory


class MovementDetector:

    def __init__(self):
        self.logger = LoggerFactory()
        self.configReader = ConfigReader()
        self.averageFrame = None
        self.motionCounter = 0
        self.text = "Unoccupied"

    @exception
    def detect_motion(self, frame):
        self.text = "Unoccupied"

        frame = imutils.resize(frame, width=500)
        gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
        gray = cv2.GaussianBlur(gray, (21, 21), 0)

        if self.averageFrame is None:
            self.averageFrame = gray.copy().astype("float")
            return self.text

        self.__check_for_movement__(gray, frame)

        return self.text

    def __check_for_movement__(self, gray, frame):
        cv2.accumulateWeighted(gray, self.averageFrame, 0.5)
        frameDelta = cv2.absdiff(gray, cv2.convertScaleAbs(self.averageFrame))
        thresh = cv2.threshold(frameDelta, self.configReader.delta_thresh, 255, cv2.THRESH_BINARY)[1]
        thresh = cv2.dilate(thresh, None, iterations=2)
        (_, contours, _) = cv2.findContours(thresh.copy(), cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
        self.__check_contour_size__(contours, frame)

    def __mark_frame__(self, frame):
        ts = datetime.now().strftime("%A %d %B %Y %I:%M:%S%p")
        cv2.putText(frame, f"Room Status: {self.text}", (10, 20), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 0, 255), 2)
        cv2.putText(frame, ts, (10, frame.shape[0] - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.35, (0, 0, 255), 1)

    def __check_contour_size__(self, contours, frame):
        for c in contours:
            if cv2.contourArea(c) >= self.configReader.min_area:
                # self.draw_contour(c, frame)
                self.text = "Occupied"
