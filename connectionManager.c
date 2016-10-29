#include "connectionManager.h"

static void fillSockAddr(int port, struct sockaddr_in *saddr);
static void closeSocket(int socket);
static void releaseWithError(char *message);
static int connectWithClient(int port);
						
static int listenerSocket = -1;
static int clientSocket = -1;

void conn_waitForClient(int port)
{
	while (connectWithClient(port) != EXIT_SUCCESS){};
}

size_t conn_read(uint8_t *buffer, size_t amount)
{
	size_t total = 0;
	while (total < amount)
	{
		ssize_t readed = recv(clientSocket, buffer + total, amount - total, 0);
		if (readed <= 0)
		{
			releaseWithError("Could not read to end.");
			return -1;
		}
		total += readed;
	}
	return total;
}

size_t conn_write(uint8_t *buffer, size_t amount)
{
	size_t total = 0;
	while (total < amount)
	{
		ssize_t sended = send(clientSocket, buffer + total, amount - total, 0);
		if (sended == -1)
		{
			releaseWithError("Could not send to end.");
			return -1;
		}
		total += sended;
	}
	return total;
}

void conn_close()
{
	closeSocket(clientSocket);
	closeSocket(listenerSocket);
}

static int connectWithClient(int port)
{
	int listenerSocket;
	if ((listenerSocket = socket(AF_INET, SOCK_STREAM, 0)) == -1)
	{
		releaseWithError("Could not open SOCKET.");
		return EXIT_FAILURE;
	}
	
	struct sockaddr_in saddr;
	socklen_t addrSize = (socklen_t) sizeof(saddr);
	fillSockAddr(port, &saddr);

	if (bind(listenerSocket, (struct sockaddr *)&saddr, addrSize) != 0)
	{
		releaseWithError("Could not bind to port.");
		return EXIT_FAILURE;
	}
	
	if (listen(listenerSocket, PENDINGS) != 0)
	{
		releaseWithError("Could not listen to socket.");
		return EXIT_FAILURE;
	}
	
	fprintf(stdout, "Start listening for clients. Port [%d]....\n", port);
	struct sockaddr_in clientAddr;

	if ((clientSocket = accept(listenerSocket, (struct sockaddr *) &clientAddr, &addrSize)) == -1)
	{
		closeSocket(clientSocket);
		closeSocket(listenerSocket);
		return EXIT_FAILURE;
	}

	closeSocket(listenerSocket);

	fprintf(stdout, "Client is connected.\n");
	return EXIT_SUCCESS;
}

	
static void fillSockAddr(int port, struct sockaddr_in *saddr)
{
	saddr->sin_family = AF_INET;
	saddr->sin_port = htons(port);
	saddr->sin_addr.s_addr = INADDR_ANY;
}

static void releaseWithError(char *message)
{
	fprintf(stderr, "EXCEPTION: '%s'", message);

	if (errno != 0)
		fprintf(stderr, " <=> '%s'", strerror(errno));

	fprintf(stderr, "\n");

	closeSocket(clientSocket);
	closeSocket(listenerSocket);
}

static void closeSocket(int socket)
{
	if (socket == -1)
		return;
	if (socket >= 0)
		close(socket);
	socket = -1;
}
