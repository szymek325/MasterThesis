import os
import shutil

from configuration_global.exception_handler import exception
from configuration_global.paths_provider import PathsProvider


class DirectoryManager:
    def __init__(self):
        self.pathProvider = PathsProvider()

    def get_files_from_directory(self, source_path):
        image_paths = [os.path.join(source_path, f) for f in os.listdir(source_path)]
        return image_paths

    def get_all_images_from_all_subdirectories(self, source_path):
        pattern = ("jpg", "png", "jpeg", "bmp")
        result = []
        for path, subdirs, files in os.walk(source_path):
            for name in files:
                if name.lower().endswith(pattern):
                    result.append(os.path.join(path, name))
        return result

    def get_subdirectories_with_files_count(self, directory_path):
        result = []
        folders = [[f, os.path.join(directory_path, f)] for f in os.listdir(directory_path)]
        for folder in folders:
            files = [os.path.join(folder[1], f) for f in os.listdir(folder[1])]
            result.append([int(folder[0]), len(files)])
        return result

    def get_filenames_from_directory(self, directory):
        files = [f for f in os.listdir(directory)]
        return files

    @exception
    def create_directory_if_doesnt_exist(self, directory):
        if not os.path.exists(directory):
            os.makedirs(directory)

    @exception
    def clean_directory(self, directory):
        if os.path.exists(directory):
            shutil.rmtree(directory)
