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
        self.logger= LoggerFactory()

    @exception
    def save_face(self, face):
        date_string = datetime.now().strftime("%Y-%m-%d-%H-%M")
        dir=self.facePath
        numberOfFiles = self.get_count_of_files_with_name(date_string,dir)
        cv2.imwrite(f"{self.facePath}/face_{date_string}-nr-{numberOfFiles+1}.jpg", face);

    @exception
    def save_motion(self, movement):
        date_string = datetime.now().strftime("%Y-%m-%d-%H-%M")
        numberOfFiles = self.get_count_of_files_with_name(date_string,self.motionPath)
        fileName=f"movement_{date_string}-nr-{numberOfFiles+1}.jpg"
        cv2.imwrite(f"{self.motionPath}/{fileName}", movement);
        self.logger.info(f"{date_string} movement detected. File saved: {fileName}")

    @exception
    def get_count_of_files_with_name(self, fileName,dir):
        filesWithFileName = [f for f in listdir(dir) if fileName in f]
        return len(filesWithFileName)
