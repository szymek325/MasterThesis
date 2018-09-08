import RPi.GPIO as gpio

from configuration_global.logger_factory import LoggerFactory


class GpioReader():
    def __init__(self):
        self.logger = LoggerFactory()
        gpio.setmode(gpio.BCM)

        def read_pin(self, number: int):

            gpio.setup(23, gpio.IN, pull_up_down=gpio.PUD_DOWN)
        gpio.read()
