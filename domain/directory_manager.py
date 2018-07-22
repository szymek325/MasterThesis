import os
import shutil

from configuration_global.paths_provider import PathsProvider


class DirectoryManager:
    def __init__(self):
        self.pathProvider = PathsProvider()

    def get_file_from_directory(self, source_path):
        self.create_directory_if_doesnt_exist(source_path)
        image_paths = [os.path.join(source_path, f) for f in os.listdir(source_path)]
        return image_paths[0]

    def get_subdirectories_with_files_count(self, directory_path):
        self.create_directory_if_doesnt_exist(directory_path)
        result = []
        folders = [[f, os.path.join(directory_path, f)] for f in os.listdir(directory_path)]
        for folder in folders:
            files = [os.path.join(folder[1], f) for f in os.listdir(folder[1])]
            result.append([int(folder[0]), len(files)])
        return result

    def create_directory_if_doesnt_exist(self, directory):
        if not os.path.exists(directory):
            os.makedirs(directory)

    def clean_directory(self, directory):
        if os.path.exists(directory):
            shutil.rmtree(directory)
