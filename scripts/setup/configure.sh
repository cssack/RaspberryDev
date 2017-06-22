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
cp configureFiles/motd ~/.motd
if grep -xq ".*cat \.motd.*" ~/.bashrc 
then 
	:
else
	echo -e "\n\n\ncat .motd" >> ~/.bashrc
	echo -e "
if [ -f readme ]; then
	echo;echo;echo
	echo readme:
	echo - - - - - - - - - - - - - - - - - - - -
	cat readme
	echo - - - - - - - - - - - - - - - -  - - - -
	echo;echo;echo
fi" >> ~/.bashrc
fi



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
sudo rm -r $targetBinFolder/* | sed -r 's/^/   /'
sudo mkdir -p $targetBinFolder/ | sed -r 's/^/   /'
sudo ln -s -r ../_bin/* $targetBinFolder/ | sed -r 's/^/   /'
sudo chmod g+rx $targetBinFolder/*



echo ">   readme"
cp readme ~/readme
