from datetime import datetime
import cv2
from os import listdir
from exception_handler.exception_handler import exception

from config_reader.config_reader import ConfigReader


class FilesManager:
    def __init__(self):
        self.configReader = ConfigReader()
        self.facePath = self.configReader.detectedFaceSavePath
        self.motionPath = self.configReader.detectedMotionPath

    @exception
    def save_face(self, face):
        date_string = datetime.now().strftime("%Y-%m-%d-%H-%M")
        dir=self.facePath
        numberOfFiles = self.get_count_of_files_with_name(date_string,dir)
        cv2.imwrite(f"{self.facePath}/face_{date_string}-nr-{numberOfFiles+1}.jpg", face);

    @exception
    def save_motion(self, movement):
        date_string = datetime.now().strftime("%Y-%m-%d-%H-%M")
        dir=self.motionPath
        numberOfFiles = self.get_count_of_files_with_name(date_string,dir)
        cv2.imwrite(f"{dir}/movement_{date_string}-nr-{numberOfFiles+1}.jpg", movement);

    @exception
    def get_count_of_files_with_name(self, fileName,dir):
        filesWithFileName = [f for f in listdir(dir) if fileName in f]
        return len(filesWithFileName)
