import os


class DirectoryManager:

    def create_directory_if_doesnt_exist(self, directory):
        if not os.path.exists(directory):
            os.makedirs(directory)
