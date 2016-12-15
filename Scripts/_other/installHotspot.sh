#!/bin/bash

echo "-> INSTALL: hostapd"
sudo apt-get -y install hostapd


echo " "
echo " "
echo " "
echo "-> INSTALL: dnsmasq"
sudo apt-get -y install dnsmasq




echo "-> DEACTIVATE: dhcpcd"

if ! sudo grep -q "^denyinterfaces wlan0" /etc/dhcpcd.conf
then
	sudo sh -c 'echo "" >> /etc/dhcpcd.conf'
	sudo sh -c 'echo "denyinterfaces wlan0" >> /etc/dhcpcd.conf'
fi




echo "-> CONFIGURE: static ip"
#Set static ip
sudo ifconfig wlan0 192.168.111.1

#Set persistent static ip
if ! sudo grep -q "^iface wlan0 inet static" /etc/network/interfaces
then
	sudo sh -c 'echo "" >> /etc/network/interfaces'
	sudo sh -c 'echo "allow-hotplug wlan0" >> /etc/network/interfaces'
	sudo sh -c 'echo "iface wlan0 inet static" >> /etc/network/interfaces'
	sudo sh -c 'echo "    address 192.168.111.1" >> /etc/network/interfaces'
	sudo sh -c 'echo "    netmask 255.255.255.0" >> /etc/network/interfaces'
	sudo sh -c 'echo "    network 192.168.111.0" >> /etc/network/interfaces'
	sudo sh -c 'echo "    broadcast 192.168.111.255" >> /etc/network/interfaces'
fi

sudo service dhcpcd restart
sudo ip addr flush dev wlan0
sudo ifdown wlan0
sudo ip addr flush dev wlan0
sudo ifup wlan0



echo "-> CONFIGURE: hostapd"
sudo cp installHotspotFiles/hostapd.conf /etc/hostapd/hostapd.conf
sudo sed -i "s/^#DAEMON_CONF.*/DAEMON_CONF=\"\/etc\/hostapd\/hostapd.conf\"/" /etc/default/hostapd



echo "-> CONFIGURE: dnsmasq"
sudo cp installHotspotFiles/dnsmasq.conf /etc/dnsmasq.conf


echo "-> START SERVICE: hostapd, dnsmasq"

sudo service hostapd start
sudo service dnsmasq start


echo "-> INSTALL: on startup"
sudo update-rc.d hostapd enable
sudo update-rc.d dnsmasq enable
