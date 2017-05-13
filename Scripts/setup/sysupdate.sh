#!/bin/bash

cd $(dirname `realpath $0`)
if [ "$(whoami)" == 'root' ]; then
	echo "DO NOT use sudo"
	exit 1;
fi


echo "SYSUPDATE > updateing apt-get"
echo "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
sudo apt-get update | sed 's/^/    /'


echo "SYSUPDATE > upgrading apt-get"
echo "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
sudo apt-get -y dist-upgrade | sed 's/^/    /'
