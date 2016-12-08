#!/bin/bash

TARGET=/home/cs/scripts/output/update.state

echo "Starting with update on $(date)"
echo "Update from $(date)" > $TARGET
echo "+++++++++++++++++++++++++++++++++++++++++++++++" >> $TARGET

echo "sudo apt-get update" >> $TARGET
echo "-------------------:" >> $TARGET
sudo apt-get update >> $TARGET

echo "sudo apt-get dist-upgrade" >> $TARGET
echo "-------------------------:" >> $TARGET
sudo apt-get -y dist-upgrade >> $TARGET

echo "+++++++++++++++++++++++++++++++++++++++++++++++" >> $TARGET

echo "FINISHED"
