from image_operators.movement_detector import MovementDetector


class Runner:

    def __init__(self):
        self.motion_detector = MovementDetector()

    def run(self):
        self.motion_detector.detect_motion()


runner = Runner()
runner.run()
