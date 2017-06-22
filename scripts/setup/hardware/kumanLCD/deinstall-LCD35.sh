#!/bin/bash

cd $(dirname `realpath $0`)
user=livescreen

if [ "$(whoami)" == 'root' ]; then
	echo "DO NOT USE sudo"
	exit 1;
fi


echo;echo;echo "remove autologin"
echo "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
if [ /etc/systemd/system/getty@tty1.service.d/autologin.conf ]; then
	sudo rm /etc/systemd/system/getty@tty1.service.d/autologin.conf
else
	echo ">   already removed"
fi



echo;echo;echo "terminate tty1 session"
echo "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
sudo sysctl -p
sudo systemctl disable serial-getty@ttyAMA0.service
sudo kill $(ps -U $user | grep tty1 | sed -r "s/([0-9]+).*/\1/") >/dev/null 2>&1
sudo kill $(ps -U $user | grep tty1 | sed -r "s/([0-9]+).*/\1/") >/dev/null 2>&1

sudo sysctl -p
sudo systemctl enable serial-getty@ttyAMA0.service

echo;echo;echo "remove $user"
echo "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
if id "$user" >/dev/null 2>&1; then
	sudo userdel -r $user
else
	echo ">   already removed"
fi


echo;echo;echo "uninstall driver"
echo "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
cd LCD-show
sudo ./LCD-hdmi
