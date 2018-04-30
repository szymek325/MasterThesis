from configuration_global.logger_factory import LoggerFactory
import RPi.GPIO as gpio

class GpioReader():
    def __init__(self):
        self.logger = LoggerFactory()
        gpio.setmode(gpio.BCM)


    def start(self):
        gpio.setup(23,gpio.IN, pull_up_down=gpio.PUD_DOWN)
        print(f"{gpio.input(23)}")


if __name__ == "__main__":
    gpio_reader = GpioReader()
    gpio_reader.start()