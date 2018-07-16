from configuration_global.logger_factory import LoggerFactory
from runners.process.face_detection_process import FaceDetectionProcess
from runners.process.face_recognition_process import FaceRecognitionProcess
from runners.process.neural_network_training_runner import NeuralNetworkTrainingRunner


class ProgramRunner:
    def __init__(self):
        self.logger = LoggerFactory()
        self.detectionProcessRunner = FaceDetectionProcess()
        self.recognitionProcessRunner = FaceRecognitionProcess()
        self.neuralNetworkTrainingProcessRunner = NeuralNetworkTrainingRunner()

    def run_program(self):
        self.logger.info("START APP")
        self.detectionProcessRunner.run_face_detection()
        self.recognitionProcessRunner.run_face_recognition()
        self.neuralNetworkTrainingProcessRunner.run_training()
        self.logger.info("END APP")


if __name__ == "__main__":
    program = ProgramRunner()
    program.run_program()
