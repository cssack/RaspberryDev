   
#ifndef CONNECTIONMANAGER
#define CONNECTIONMANAGER

#include <stdio.h>
#include <unistd.h>
#include <stdlib.h>
#include <string.h>
#include <signal.h>
#include <errno.h>

#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>


#define PENDINGS (1)

/**
 * @brief used for openening the server, listening on port, waiting for a client to connect.
 * @param port the port the server will listen on for clients.
 **/
void conn_waitForClient(int port);
/**
 * @brief used for reading from client connection
 * @param buffer the target buffer where the message will be written to.
 * @param amount the amount of bytes to read from client.
 **/
size_t conn_read(uint8_t *buffer, size_t amount);

/**
 * @brief used for sending to client connection
 * @param buffer the target buffer where the message will be read from.
 * @param amount the amount of bytes to send to client.
 **/
size_t conn_write(uint8_t *buffer, size_t amount);

/**
 * @brief used for releasing all connection relevant resources.
 **/
void conn_close();
#endif
