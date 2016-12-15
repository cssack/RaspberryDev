#!/bin/bash


get_script_dir () {
	SOURCE="${BASH_SOURCE[0]}"
	while [ -h "$SOURCE" ]; do
		DIR="$( cd -P "$( dirname "$SOURCE" )" && pwd )"
		SOURCE="$( readlink "$SOURCE" )"
		[[ $SOURCE != /* ]] && SOURCE="$DIR/$SOURCE"
	done
	DIR="$( cd -P "$( dirname "$SOURCE" )" && pwd )"
	echo "$DIR"
}



DIR="$(get_script_dir)"

echo "*************************  GIT PULL        ************************"
git -C "$DIR" pull

echo "*************************  COPYING FILES   ************************"

echo "--> COPY .vimrc"
cp $DIR/_configFiles/vimrc ~/.vimrc
sudo cp $DIR/_configFiles/vimrc /root/.vimrc

echo "--> COPY motd"
sudo cp $DIR/_configFiles/motd /etc/motd

echo "--> COPY selected_editor"
cp $DIR/_configFiles/selected_editor ~/.selected_editor
sudo cp $DIR/_configFiles/selected_editor /root/.selected_editor

echo "--> COPY jail.local"
sudo cp $DIR/_configFiles/jail.local /etc/fail2ban/jail.local

echo "--> COPY ieee-oui.txt"
sudo cp $DIR/_configFiles/ieee-oui.txt /usr/share/arp-scan/ieee-oui.txt
 
echo "--> COPY .gitconfig"
cp -n $DIR/_configFiles/gitconfig ~/.gitconfig

echo "--> COPY bin files"
mkdir -p ~/bin/
ln -s $DIR/_binFiles/* ~/bin/
ln -s "$DIR/copyFiles.sh" "${HOME}/bin/cs_reload"

echo "--> COPY README"
cp $DIR/README ~/README
