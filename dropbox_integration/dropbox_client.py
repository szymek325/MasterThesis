import dropbox

from dropbox_integration.configuration.config_reader import ConfigReader


class DropboxClient:

    def __init__(self):
        self.config=ConfigReader()
        self.client=dropbox.Dropbox(self.config.dropbox_access_token)

    def get_file(self,requestId):
        file,meta=self.client.files_download(f"{self.config.face_detection_jobs_path}/{requestId}/4700_1h.PNG")
        print(self.client.users_get_current_account())



if __name__ == "__main__":
    drop = DropboxClient()
    drop.get_file(3)