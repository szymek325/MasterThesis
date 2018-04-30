import platform
import urllib.parse

from dataLayer.configuration.config_reader import ConfigReader


class ConnectionStringProvider():
    def __init__(self):
        self.configReader = ConfigReader()
        self.local = self.configReader.use_local_db

    def get_connection_string(self):

        if platform.system() == "Windows":
            driver = '{ODBC Driver 13 for SQL Server}'
        else:
            driver = '{FreeTDS}'
            self.local = False

        if self.local:
            db_server = self.configReader.local_db_server
            db_name = self.configReader.local_db_name
            login = self.configReader.local_login
            password = self.configReader.local_password
        else:
            db_server = self.configReader.db_server
            db_name = self.configReader.db_name
            login = self.configReader.db_login
            password = self.configReader.db_password

        connection_string = urllib.parse.quote_plus(
            f"DRIVER={driver};Server={db_server};Database={db_name};"
            f"UID={login};PWD={password};TDS_Version=8.0;Port=1433;")

        return connection_string
