from configuration_global.config_reader import ConfigReader


class EnvironmentsProvider():

    def __init__(self):
        self.configReader = ConfigReader()
        self.environment = self.configReader.environment_to_use

    @property
    def debug(self):
        return "Debug"

    @property
    def azure(self):
        return "Azure"

    @property
    def amazon(self):
        return "Amazon"

    def get_environment(self):
        if self.environment.lower() == self.debug.lower():
            return self.debug
        elif self.environment.lower() == self.azure.lower():
            return self.azure
        elif self.environment.lower() == self.amazon.lower():
            return self.amazon
        else:
            return "unrecognized"
