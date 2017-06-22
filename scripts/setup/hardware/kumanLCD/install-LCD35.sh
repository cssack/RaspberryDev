#!/bin/bash

cd $(dirname `realpath $0`)
user=livescreen

if [ "$(whoami)" == 'root' ]; then
	echo "DO NOT USE sudo"
	exit 1;
fi



echo "adduser $user"
echo "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
if id "$user" >/dev/null 2>&1; then
	echo ">   already installed"
else
	id -u $user &>/dev/null || sudo useradd $user 
	read -s -p ">   enter password for user $user: " pw
	echo
	echo "$user:$pw" | chpasswd

	sudo adduser $user sudo
	sudo mkhomedir_helper $user
	sh -c "echo '$user ALL=NOPASSWD: ALL' >> /etc/sudoers"
	echo ">   completed"
fi


echo;echo;echo "configure autologin"
echo "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
if grep -xq ".*\?--autologin ${user}.*" /etc/systemd/system/getty@tty1.service.d/autologin.conf; then
	echo ">   already configured"
else
	sudo sh -c "echo \"[Service]\nExecStart=\nExecStart=-/sbin/agetty --autologin $user --noclear %I 38400 linux\" > /etc/systemd/system/getty@tty1.service.d/autologin.conf"
	echo ">   completed"
fi


echo;echo;echo "apply cs group ownership"
echo "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
sudo chgrp -R cs /home/$user
sudo chmod -R +w /home/$user
echo ">   completed"

echo;echo;echo "install driver"
echo "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
cd LCD-show
sudo ./LCD35-show
