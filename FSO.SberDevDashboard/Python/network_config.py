import network
import ntptime
import config
import time

def netconfig():
	wlan = network.WLAN(network.STA_IF)
	wlan.active(True)

	if not wlan.isconnected():
		networks = wlan.scan()
		for net in networks:
			print(net[0])
			if net[0] in config.SSID:
				print('connecting to network: ', net[0])
				wlan.connect(net[0], config.SSID[net[0]])
				while not wlan.isconnected():
					pass

				break

	print('network config:', wlan.ifconfig())

	for ntpServer in config.NTPSERVERS:
		try:
			ntptime.host = ntpServer
			ntptime.settime()
			print('Time syncronized: ', time.localtime(time.time()))
			break;
		except:
			pass
