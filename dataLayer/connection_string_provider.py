import platform
import urllib.parse

from configuration_global.config_reader import ConfigReader
from configuration_global.environments_provider import EnvironmentsProvider


class ConnectionStringProvider():
    def __init__(self):
        self.configReader = ConfigReader()
        self.environmentsProvider = EnvironmentsProvider()
        self.environment = self.configReader.environment_to_use
        self.db_server = "db_server"
        self.db_name = "db_name"
        self.login = "login"
        self.password = "password"

    def get_connection_string(self):

        if platform.system() == "Windows":
            driver = '{ODBC Driver 13 for SQL Server}'
        else:
            driver = '{FreeTDS}'

        if self.environment.lower() == self.environmentsProvider.debug.lower():
            self.local_configuration()
        elif self.environment.lower() == self.environmentsProvider.azure.lower():
            self.azure_configuration()
        elif self.environment.lower() == self.environmentsProvider.amazon.lower():
            self.amazon_configuration()
        else:
            self.local_configuration()

        connection_string = urllib.parse.quote_plus(f"DRIVER={driver};Server={self.db_server};Database={self.db_name};"
                                                    f"UID={self.login};PWD={self.password};TDS_Version=8.0;Port=1433;")

        return connection_string

    def amazon_configuration(self):
        self.db_server = "masterthesisdb.crgqwbbi5jyz.eu-west-2.rds.amazonaws.com"
        self.db_name = "masterthesisdb"
        self.login = "testAccount"
        self.password = "drift11"

    def azure_configuration(self):
        self.db_server = "masterthesi.database.windows.net"
        self.db_name = "masterthesisdb"
        self.login = "testAccount"
        self.password = "TestPassword1"

    def local_configuration(self):
        self.db_server = "(LocalDb)\\MSSQLLocalDB"
        self.db_name = "TestDb"
        self.login = "testAccount"
        self.password = "drift11"
