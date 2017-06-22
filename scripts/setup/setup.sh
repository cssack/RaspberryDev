#!/bin/bash

cd $(dirname `realpath $0`)
user=cs


if [ "$(whoami)" == 'root' ]; then
	if id "$user" >/dev/null 2>&1; then
		echo "Switch to $user user."
		exit 1;
	fi


	echo "adduser $user"
	echo "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
	id -u $user &>/dev/null || useradd $user 
	read -s -p "enter password for user $user: " pw
	echo
	echo "$user:$pw" | chpasswd
	adduser $user sudo
	sh -c "echo '$user ALL=NOPASSWD: ALL' >> /etc/sudoers"
	mkhomedir_helper $user
	echo ">>>>>>>>> change to user '$user' <<<<<<<<<<"
	echo "then execute setup.sh"
	exit 1;
fi

if [ "$(whoami)" != "$user" ]; then
	echo ">>>>>>>>> change to user '$user' <<<<<<<<<<"
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
