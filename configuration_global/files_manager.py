from datetime import datetime
import cv2
from os import listdir, path
from configuration_global.exception_handler import exception

from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory


class FilesManager:
    def __init__(self):
        self.configReader = ConfigReader()
        self.facePath = self.configReader.detected_face_save_path
        self.motionPath = self.configReader.detected_motion_path
        self.trainingDataPath = self.configReader.training_data
        self.logger = LoggerFactory()

    @exception
    def save_face(self, face, fileName):
        cv2.imwrite(f"{self.facePath}/{fileName}.jpg", face);
        self.logger.info(f"     File saved: {fileName}.jpg")

    @exception
    def save_motion(self, movement):
        fileName = f"movement_{datetime.now().strftime('%Y-%m-%d-%H-%M')}_nr-"
        numberOfFiles = self.get_count_of_files_with_name(fileName, self.motionPath)
        fileName += str((numberOfFiles + 1)) + ".jpg"
        cv2.imwrite(f"{self.motionPath}/{fileName}", movement);
        self.logger.info(f"Movement detected. File saved: {fileName}")

    @exception
    def get_count_of_files_with_name(self, fileName, dir):
        filesWithFileName = [f for f in listdir(dir) if fileName in f]
        return len(filesWithFileName)

    def get_unprocessed_files(self):
        unprocessedFiles = [f for f in listdir(self.motionPath)]
        return unprocessedFiles

    def get_training_data(self):
        imagePaths = [path.join(self.trainingDataPath, f) for f in listdir(self.trainingDataPath)]
        return imagePaths
