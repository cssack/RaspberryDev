#!/bin/bash


cd $(dirname `realpath $0`)
if [ "$(whoami)" == 'root' ]; then
	echo "DO NOT use sudo"
	exit 1;
fi

targetBinFolder=/usr/local/bin
echo "PULL repository"
echo "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
git pull | sed 's/^/    /'
echo



echo "CONFIGURE"
echo "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
echo ">   .vimrc"
cp configureFiles/vimrc ~/.vimrc
sudo cp configureFiles/vimrc /root/.vimrc



echo ">   motd"
sudo cp configureFiles/motd /etc/motd



echo ">   selected_editor"
cp configureFiles/selected_editor ~/.selected_editor
sudo cp configureFiles/selected_editor /root/.selected_editor



echo ">   jail.local"
sudo cp configureFiles/jail.local /etc/fail2ban/jail.local



echo ">   ieee-oui.txt"
sudo cp configureFiles/ieee-oui.txt /usr/share/arp-scan/ieee-oui.txt


 
echo ">   gitconfig"
cp -n configureFiles/gitconfig ~/.gitconfig



echo ">   $targetBinFolder"
rm -r $targetBinFolder/* | sed -r 's/^/   /'
mkdir -p $targetBinFolder/ | sed -r 's/^/   /'
ln -s -r ../_bin/* $targetBinFolder/ | sed -r 's/^/   /'



echo ">   readme"
cp README ~/README
