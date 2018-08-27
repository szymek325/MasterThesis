import cv2
from datetime import datetime
from configuration_global.logger_factory import LoggerFactory
from opencv_client.image_converters.image_editor import ImageEditor
from opencv_client.movement_detection.movement_detector import MovementDetector
from raspberry_modules.camera_operator import CameraOperator


class MotionDetectionOperator:
    def __init__(self):
        self.logger = LoggerFactory()
        self.cameraOperator = CameraOperator()
        self.movementDetector = MovementDetector()
        self.imageEditor = ImageEditor()
        self.lastUploaded = datetime.now()

    def run_detection(self):
        self.cameraOperator.init_camera()
        while True:
            self.stateOfRoom = "Unoccupied"
            frame = self.cameraOperator.grab_frame()

            movements = self.movementDetector.detect_motion(frame)
            if len(movements) is not 0:
                self.stateOfRoom = "Occupied"

            if self.configReader.movement_timestamp:
                self.imageEditor.mark_frame(frame, self.stateOfRoom)
            if self.configReader.movement_marking:
                self.imageEditor.draw_contour(movements, frame)

            self.__save_frame_if_room_is_occupied__(frame)

            # if should bne here
            self.cameraOperator.show_video()

            key = cv2.waitKey(1) & 0xFF
            if key == ord("q"):
                break
        self.cameraOperator.release_camera()

    def __save_frame_if_room_is_occupied__(self, frame):
        if self.stateOfRoom == "Occupied":
            if (datetime.now() - self.lastUploaded).seconds >= 2.5:
                self.motionCounter += 1
                if self.motionCounter >= self.configReader.min_motion_frames:
                    self.filesManager.save_motion(frame)
        else:
            self.motionCounter = 0