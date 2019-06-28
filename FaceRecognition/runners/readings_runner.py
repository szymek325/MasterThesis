from domain.readings.sensors_operator import SensorsOperator


class ReadingsRunner():
    def __init__(self):
        self.sensorsOperator = SensorsOperator()

    def run(self):
        self.sensorsOperator.add_readings()


if __name__ == "__main__":
    program = ReadingsRunner()
    program.run()
