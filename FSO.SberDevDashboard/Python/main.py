from machine import Pin
import config

def http_get(url):
    import socket
    _, _, host, path = url.split('/', 3)
    addr = socket.getaddrinfo(host, 80)[0][-1]
    s = socket.socket()
    s.connect(addr)
    s.send(bytes('GET /%s HTTP/1.0\r\nHost: %s\r\n\r\n' % (path, host), 'utf8'))
    while True:
        data = s.recv(100)
        if data:
            print(str(data, 'utf8'), end='')
        else:
            break
    s.close()

if pin.value() > 0:
    from neopixel import NeoPixel

    import ws2812

    pin = Pin(4, Pin.OUT)
    np = NeoPixel(pin, 1)
    ws2812.demo(np)

    #wait to measure event
    import time
    import urequests

    while True:
        url = 'http://172.30.14.84/FSO.SDD.NativeWebApi/api/lastbuildstatus'
        print('check url: %s' % url)
        r = urequests.get(url).json()

        print(r["status"])

        if r["status"] == False:
            ws2812.demo(np)

        #http_get(url)
        time.sleep(10)

else:
    print('skip main...')
