from configuration_global.logger_factory import LoggerFactory
from configuration_global.paths_provider import PathsProvider
from dataLayer.repositories.person_repository import PersonRepository
from domain.directory_manager import DirectoryManager
from dropbox_integration.files_downloader import FilesDownloader


class PeopleManager():
    def __init__(self):
        self.logger = LoggerFactory()
        self.pathProvider = PathsProvider()
        self.directoryManager = DirectoryManager()
        self.peopleRepo = PersonRepository()
        self.filesDownloader = FilesDownloader()

    def download_people_if_some_missing(self):
        people_path = self.pathProvider.local_person_image_path()
        directories = self.directoryManager.get_all_subdirectories(people_path)
        print(directories)
        people = self.peopleRepo.get_people_ids_with_images_count()
        print(people)
        for p in people:
            self.filesDownloader.download_person((p[0]))
