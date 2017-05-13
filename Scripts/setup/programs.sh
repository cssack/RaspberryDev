#!/bin/bash

cd $(dirname `realpath $0`)
if [ "$(whoami)" == 'root' ]; then
	echo "DO NOT use sudo"
	exit 1;
fi

GREEN='\033[0;32m'
NC='\033[0m'

for prog in fail2ban vim iperf3 tree screen tcpdump nmap arp-scan libxml2-utils
do
	echo -e "PROGRAMS > $GREEN$prog$NC"
	echo "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
	sudo apt-get --assume-yes install $prog | sed 's/^/    /'
	echo " "
	echo " "
	echo " "
	echo " "
done
