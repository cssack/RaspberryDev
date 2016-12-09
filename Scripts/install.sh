

echo " "
echo " "
echo " "
echo "*************************  UPDATEING APT-GET  *********************"
sudo apt-get update


echo " "
echo " "
echo " "
echo "*************************  UPGRADING APT-GET  *********************"
sudo apt-get upgrade -y


echo " "
echo " "
echo " "
echo "*************************  INSTALLING fail2ban ********************"
sudo apt-get --assume-yes install fail2ban


echo " "
echo " "
echo " "
echo "*************************  INSTALLING vim *************************"
sudo apt-get --assume-yes install vim


echo " "
echo " "
echo " "
echo "*************************  INSTALLING iperf3 **********************"
sudo apt-get --assume-yes install iperf3


echo " "
echo " "
echo " "
echo "*************************  INSTALLING tree ************************"
sudo apt-get --assume-yes install tree


echo " "
echo " "
echo " "
echo "*************************  INSTALLING screen **********************"
sudo apt-get --assume-yes install screen


echo " "
echo " "
echo " "
echo "*************************  INSTALLING tcpdump *********************"
sudo apt-get --assume-yes install tcpdump

echo " "
echo " "
echo " "
echo " "
echo " "
echo " "

cp ./files/vimrc ~/.vimrc
echo ".vimrc created"
cp ./files/motd /etc/motd
echo "motd created"
cp ./files/selected_editor ~/.selected_editor
echo "selected_editor created"
cp ./files/jail.local /etc/fail2ban/jail.local
echo "jail.local created"
