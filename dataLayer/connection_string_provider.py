import platform
import urllib.parse

from configuration_global.config_reader import ConfigReader


class ConnectionStringProvider():
    def __init__(self):
        self.configReader = ConfigReader()
        self.local = self.configReader.use_local_environment

    def get_connection_string(self):

        if platform.system() == "Windows":
            driver = '{ODBC Driver 13 for SQL Server}'
        else:
            driver = '{FreeTDS}'
            self.local = False

        if self.local:
            db_server = "(LocalDb)\\MSSQLLocalDB"
            db_name = "TestDb"
            login = "testAccount"
            password = "drift11"
        else:
            db_server = "den1.mssql6.gear.host"
            db_name = "masterthesisdb"
            login = "masterthesisdb"
            password = "Zp9P?Q!ezuXH"

        connection_string = urllib.parse.quote_plus(
            f"DRIVER={driver};Server={db_server};Database={db_name};"
            f"UID={login};PWD={password};TDS_Version=8.0;Port=1433;")

        return connection_string
