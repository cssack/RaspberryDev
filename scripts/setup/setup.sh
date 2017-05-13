#!/bin/bash

cd $(dirname `realpath $0`)
if [ "$(whoami)" == 'root' ]; then
	echo "DO NOT use sudo"
	exit 1;
fi

if [ "$(whoami)" != 'cs' ]; then
	if id "cs" >/dev/null 2>&1; then
		echo "Switch to cs user."
		exit 1;
	fi


	id -u cs &>/dev/null || useradd cs 
	sudo adduser cs sudo
	sudo sh -c "echo 'cs ALL=NOPASSWD: ALL' >> /etc/sudoers"
	echo "change to user 'cs'"
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
