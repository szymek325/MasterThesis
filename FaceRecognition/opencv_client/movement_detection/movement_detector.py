import cv2
import imutils
from configuration_global.exception_handler import exception
from configuration_global.logger_factory import LoggerFactory

MIN_AREA = 500
DELTA_TRESH = 25


class MovementDetector:

    def __init__(self):
        self.logger = LoggerFactory()
        self.averageFrame = None

    @exception
    def detect_motion(self, frame):
        """
        :param: image/frame: cv.imread
        :return: contours: list of contours of movement
        """
        frame = imutils.resize(frame, width=500)
        gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
        gray = cv2.GaussianBlur(gray, (21, 21), 0)

        if self.averageFrame is None:
            self.averageFrame = gray.copy().astype("float")
            return []

        movements = self.__check_for_movement__(gray)
        validated_contours = self.__check_contour_size__(movements)
        return validated_contours

    def __check_for_movement__(self, gray):
        cv2.accumulateWeighted(gray, self.averageFrame, 0.5)
        frame_delta = cv2.absdiff(gray, cv2.convertScaleAbs(self.averageFrame))
        thresh = cv2.threshold(frame_delta, DELTA_TRESH, 255, cv2.THRESH_BINARY)[1]
        thresh = cv2.dilate(thresh, None, iterations=2)
        (_, contours, _) = cv2.findContours(thresh.copy(), cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
        return contours

    def __check_contour_size__(self, contours):
        validated = []
        for c in contours:
            if cv2.contourArea(c) >= MIN_AREA:
                validated.append(c)
        return validated
