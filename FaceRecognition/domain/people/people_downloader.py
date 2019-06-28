from configuration_global.logger_factory import LoggerFactory
from configuration_global.paths_provider import PathsProvider
from dataLayer.repositories.person_repository import PersonRepository
from domain.directory_manager import DirectoryManager
from dropbox_integration.files_downloader import FilesDownloader


class PeopleDownloader():
    def __init__(self):
        self.logger = LoggerFactory()
        self.pathProvider = PathsProvider()
        self.directoryManager = DirectoryManager()
        self.peopleRepo = PersonRepository()
        self.filesDownloader = FilesDownloader()

    def download_people_to_local(self):
        people_path = self.pathProvider.local_person_image_path()
        directories = self.directoryManager.get_subdirectories_with_files_count(people_path)
        people = self.peopleRepo.get_people_ids_with_images_count()
        people_to_download = [x for x in people if x not in directories]
        self.logger.info(f"directories {directories} "
                         f"\npeople: {people}"
                         f"\npeople_to_download: {people_to_download}")
        for p in people_to_download:
            self.filesDownloader.download_person((p[0]))
