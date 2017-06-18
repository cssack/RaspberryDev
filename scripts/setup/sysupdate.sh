#!/bin/bash

cd $(dirname `realpath $0`)
if [ "$(whoami)" == 'root' ]; then
	echo "DO NOT use sudo"
	exit 1;
fi


echo "SYSUPDATE > updateing apt-get"
echo "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
sudo apt-get update | sed 's/^/    /'

echo "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
echo "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
echo "use following command to do a dist-upgrade"
echo "    BE CAREFUL"
echo "    sudo apt-get -y dist-upgrade"
echo "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
echo "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
