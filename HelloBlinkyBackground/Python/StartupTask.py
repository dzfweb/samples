import _wingpio as gpio
import time

led_pin_one = 5
led_pin_two = 6
sensor_pin = 4
pinOneValue = gpio.HIGH
pinTwoValue = gpio.LOW

gpio.setup(led_pin_one, gpio.OUT, gpio.PUD_OFF, gpio.HIGH)
gpio.setup(led_pin_two, gpio.OUT, gpio.PUD_OFF, gpio.HIGH)

def work():
    count = 0
    gpio.setup(sensor_pin, gpio.OUT)
    gpio.output(sensor_pin, gpio.LOW)
    time.sleep(0.1)

    gpio.setup(sensor_pin, gpio.IN)

    while (gpio.input(sensor_pin) == gpio.LOW):
        count += 1

    if (count > 10000):
        gpio.output(led_pin_one, gpio.HIGH)
        gpio.output(led_pin_two, gpio.LOW)
    else:
        gpio.output(led_pin_one, gpio.LOW)
        gpio.output(led_pin_two, gpio.HIGH)

    return count;

try:
    while True:
        work()
except KeyboardInterrupt:
    pass
finally:
    gpio.cleanup()