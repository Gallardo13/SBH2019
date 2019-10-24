from machine import Pin

import time
import webrepl
import network_config

# wait for user to maybe turn GPIO5 to lower state
time.sleep(2)
pin = Pin(5, Pin.IN, Pin.PULL_UP)

if pin.value() > 0:
	print('booting...')
	network_config.netconfig()

else:
	print('skip network config')

webrepl.start()
