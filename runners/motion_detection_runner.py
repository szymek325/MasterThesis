from domain.motion_detection.motion_detection_operator import MotionDetectionOperator


class MotionDetectionRunner:
    def __init__(self):
        self.motionDetectionOperator = MotionDetectionOperator()

    def run(self):
        self.motionDetectionOperator.run_detection()


if __name__ == "__main__":
    program = MotionDetectionRunner()
    program.run()
