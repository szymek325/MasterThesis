import time
import cv2
import imutils
from datetime import datetime
from configuration_global.exception_handler import exception
from configuration_global.files_manager import FilesManager
from configuration_global.logger_factory import LoggerFactory
from motionDetection.configuration.config_reader import ConfigReader
from motionDetection.movement_detector import MovementDetector


class MovementDetectorRunner:

    def __init__(self):
        self.logger = LoggerFactory()
        self.filesManager = FilesManager()
        self.configReader = ConfigReader()
        self.movementDetector = MovementDetector()
        self.stateOfRoom = "Unoccupied"
        self.lastUploaded = datetime.now()

    @exception
    def detect_motion_on_video(self):
        self.__init_camera__()
        while True:
            self.stateOfRoom = "Unoccupied"
            grabbed, frame = self.camera.read()

            if not grabbed:
                self.logger.error("Camera didn't grab a frame, raising error")
                break

            movements = self.movementDetector.detect_motion(frame)
            if len(movements) is not 0:
                self.stateOfRoom = "Occupied"

            if self.configReader.movement_timestamp:
                self.__mark_frame__(frame)
            if self.configReader.movement_marking:
                self.__draw_contour__(movements, frame)

            self.__save_frame_if_room_is_occupied__(frame)

            if self.configReader.show_video:
                cv2.imshow("Security Feed", frame)
                key = cv2.waitKey(1) & 0xFF
                if key == ord("q"):
                    break

        self.camera.release()
        cv2.destroyAllWindows()

    @exception
    def __init_camera__(self):
        if self.configReader.pi_camera_used:
            print("Picamera cannot be launch on windows")
            raise Exception("Picamera cannot be launch on windows")
        # COnfiguration for Raspberry
        # camera = PiCamera()
        # camera.resolution = tuple(conf["resolution"])
        # camera.framerate = conf["fps"]
        # rawCapture = PiRGBArray(camera, size=tuple(conf["resolution"]))
        else:
            self.camera = cv2.VideoCapture(self.configReader.camera_port)
        time.sleep(self.configReader.camera_warmup_time)

    def __save_frame_if_room_is_occupied__(self, frame):
        if self.stateOfRoom == "Occupied":
            if (datetime.now() - self.lastUploaded).seconds >= 2.5:
                self.motionCounter += 1
                if self.motionCounter >= self.configReader.min_motion_frames:
                    self.filesManager.save_motion(frame)
        else:
            self.motionCounter = 0

    def __mark_frame__(self, frame):
        ts = datetime.now().strftime("%A %d %B %Y %I:%M:%S%p")
        cv2.putText(frame, f"Room Status: {self.stateOfRoom}", (10, 20), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 0, 255), 2)
        cv2.putText(frame, ts, (10, frame.shape[0] - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.35, (0, 0, 255), 1)

    @staticmethod
    def __draw_contour__(contours, frame):
        for c in contours:
            (x, y, w, h) = cv2.boundingRect(c)
            cv2.rectangle(frame, (x, y), (x + w, y + h), (0, 255, 0), 2)


if __name__ == "__main__":
    motion_detector = MovementDetectorRunner()
    motion_detector.detect_motion_on_video()
