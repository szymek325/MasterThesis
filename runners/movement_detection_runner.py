from domain.movement_detection.movement_detection_operator import MovementDetectionOperator


class MovementDetectionRunner:
    def __init__(self):
        self.motionDetectionOperator = MovementDetectionOperator()

    def run(self):
        self.motionDetectionOperator.run_detection()


if __name__ == "__main__":
    program = MovementDetectionRunner()
    program.run()
