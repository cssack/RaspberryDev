#!/bin/bash

cd $(dirname `realpath $0`)
if [ "$(whoami)" == 'root' ]; then
	if id "cs" >/dev/null 2>&1; then
		echo "Switch to cs user."
		exit 1;
	fi


	echo "adduser cs"
	echo "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
	id -u cs &>/dev/null || useradd cs 
	read -s -p "enter password for user cs: " pw
	echo
	echo "cs:$pw" | chpasswd
	adduser cs sudo
	sh -c "echo 'cs ALL=NOPASSWD: ALL' >> /etc/sudoers"
	mkhomedir_helper cs
	echo ">>>>>>>>> change to user 'cs' <<<<<<<<<<"
	echo "then execute setup.sh"
	exit 1;
fi

if [ "$(whoami)" != 'cs' ]; then
	echo ">>>>>>>>> change to user 'cs' <<<<<<<<<<"
	exit 1;
fi


if id "pi" >/dev/null 2>&1; then

	echo "DELETE USER: pi"
	echo "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
	sudo userdel pi
	sudo rm -rf /home/pi

	echo
	echo
fi

./sysupdate.sh
./programs.sh
./configure.sh
