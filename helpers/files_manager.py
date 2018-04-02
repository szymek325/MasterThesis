from datetime import datetime
import cv2
from os import listdir
from helpers.exception_handler import exception

from helpers.config_reader import ConfigReader
from helpers.logger_factory import LoggerFactory


class FilesManager:
    def __init__(self):
        self.configReader = ConfigReader()
        self.facePath = self.configReader.detectedFaceSavePath
        self.motionPath = self.configReader.detectedMotionPath
        self.logger = LoggerFactory()

    @exception
    def save_face(self, face, fileName):
        cv2.imwrite(f"{self.facePath}/{fileName}.jpg", face);
        self.logger.info(f"     File saved: {fileName}.jpg")

    @exception
    def save_motion(self, movement):
        fileName = f"movement_{datetime.now().strftime('%Y-%m-%d-%H-%M')}-nr-"
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
