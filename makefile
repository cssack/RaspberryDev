APPNAME=server
DIR=bin
FLAGS=-std=c99 -pedantic -Wall -D_XOPEN_SOURCE=500 -D_BSD_SOURCE -g

all: server.o connectionManager.o
	gcc $(FLAGS) -o '$(DIR)/$(APPNAME)' '$(DIR)/server.o' '$(DIR)/connectionManager.o'

clear:
	rm *.o

server.o: server.c server.h
	gcc $(FLAGS) -c -o $(DIR)/$@ $<

connectionManager.o: connectionManager.c connectionManager.h
	gcc $(FLAGS) -c -o $(DIR)/$@ $<

gpioManager.o: gpioManager.c gpioManager.h
	gcc $(FLAGS) -lwiringPi -c -o $(DIR)/$@ $<
