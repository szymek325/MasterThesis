import cv2
from datetime import datetime
from configuration_global.logger_factory import LoggerFactory
from domain.movement_detection.movement_result_operator import MovementResultOperator
from opencv_client.movement_detection.movement_detector import MovementDetector
from raspberry_modules.camera_operator import CameraOperator

MIN_MOTION_FRAMES = 8


class MovementDetectionOperator:
    def __init__(self):
        self.logger = LoggerFactory()
        self.cameraOperator = CameraOperator()
        self.movementDetector = MovementDetector()
        self.resultOperator = MovementResultOperator()
        self.lastUploaded = datetime.now()

    def run_detection(self):
        self.cameraOperator.init_camera()
        while True:
            self.stateOfRoom = "Unoccupied"
            frame = self.cameraOperator.grab_frame()

            movements = self.movementDetector.detect_motion(frame)
            if len(movements) is not 0:
                self.stateOfRoom = "Occupied"

            self.__save_frame_if_room_is_occupied__(frame, movements)

            # if should bne here
            self.cameraOperator.show_video(frame)
            key = cv2.waitKey(1) & 0xFF
            if key == ord("q"):
                break
        self.cameraOperator.release_camera()

    def __save_frame_if_room_is_occupied__(self, frame, movements):
        if self.stateOfRoom == "Occupied":
            self.motionCounter += 1
            if (datetime.now() - self.lastUploaded).seconds >= 2:
                if self.motionCounter >= MIN_MOTION_FRAMES:
                    self.resultOperator.prepare_and_upload_result(frame, movements)
                    self.lastUploaded=datetime.now()
        else:
            self.motionCounter = 0
