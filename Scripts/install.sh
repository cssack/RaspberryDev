echo " "
echo " "
echo " "
echo "*************************  UPDATEING APT-GET  *********************"
sudo apt-get update


echo " "
echo " "
echo " "
echo "*************************  UPGRADING APT-GET  *********************"
sudo apt-get -y dist-upgrade


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
echo "*************************  INSTALLING nmap ************************"
sudo apt-get --assume-yes install nmap

echo " "
echo " "
echo " "
echo " "
echo " "
echo " "
echo " "

./copyFiles.sh
