import json


class ConfigReader:

    def __init__(self):
        self.configuration = json.load(open("dataLayer/configuration/config.json"))

    @property
    def use_local_db(self):
        return self.configuration["use_local_db"]

    @property
    def linux_driver(self):
        return self.configuration["linux_driver"]

    @property
    def windows_driver(self):
        return self.configuration["windows_driver"]

    @property
    def db_login(self):
        return self.configuration['gear_host_db']["login"]

    @property
    def db_password(self):
        return self.configuration['gear_host_db']["password"]

    @property
    def local_login(self):
        return self.configuration['local_db']["login"]

    @property
    def local_password(self):
        return self.configuration['local_db']["password"]

    @property
    def db_name(self):
        return self.configuration['gear_host_db']["db_name"]

    @property
    def local_db_name(self):
        return self.configuration['local_db']["db_name"]

    @property
    def db_server(self):
        return self.configuration['gear_host_db']["server"]

    @property
    def local_db_server(self):
        return self.configuration['local_db']["server"]
