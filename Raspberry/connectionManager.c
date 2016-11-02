#include "connectionManager.h"

static void fillSockAddr(int port, struct sockaddr_in *saddr);
static void closeSocket(int socket);
static void releaseWithError(char *message);
static int connectWithClient(int port);
						
static int listenerSocket = -1;
static int clientSocket = -1;

void conn_waitForClient(int port)
{
	while (connectWithClient(port) == CONNERROR)
	{
		sleep(1);
	}
}

int conn_read(char* buffer, size_t amount)
{
	size_t total = 0;
	while (total < amount)
	{
		ssize_t readed = recv(clientSocket, buffer + total, amount - total, 0);
		if (readed <= 0)
		{
			releaseWithError("Could not read to end.");
			return CONNERROR;
		}
		total += readed;
	}
	return total;
}

int conn_write(char* buffer, size_t amount)
{
	size_t total = 0;
	while (total < amount)
	{
		ssize_t sended = send(clientSocket, buffer + total, amount - total, 0);
		if (sended == -1)
		{
			releaseWithError("Could not send to end.");
			return CONNERROR;
		}
		total += sended;
	}
	return total;
}

void conn_close()
{
	closeSocket(listenerSocket);
	closeSocket(clientSocket);
}

static int connectWithClient(int port)
{
	int listenerSocket;
	if ((listenerSocket = socket(AF_INET, SOCK_STREAM, 0)) == -1)
	{
		releaseWithError("Could not open SOCKET.");
		return CONNERROR;
	}

	int value = 1;
	if (setsockopt(listenerSocket, SOL_SOCKET, SO_REUSEADDR, &value, sizeof(value)) != 0)
	{
		releaseWithError("Could not set socket option");
		return CONNERROR;
	}

	
	struct sockaddr_in saddr;
	socklen_t addrSize = (socklen_t) sizeof(saddr);
	fillSockAddr(port, &saddr);

	if (bind(listenerSocket, (struct sockaddr *)&saddr, addrSize) != 0)
	{
		releaseWithError("Could not bind to port.");
		return CONNERROR;
	}
	
	if (listen(listenerSocket, PENDINGS) != 0)
	{
		releaseWithError("Could not listen to socket.");
		return CONNERROR;
	}
	
	fprintf(stdout, "\n\n\nStart listening for clients. Port [%d]....\n", port);
	struct sockaddr_in clientAddr;

	if ((clientSocket = accept(listenerSocket, (struct sockaddr *) &clientAddr, &addrSize)) == -1)
	{
		releaseWithError("Client could not connect.");
		return CONNERROR;
	}

	closeSocket(listenerSocket);

	fprintf(stdout, "Client [%s] is connected.\n", inet_ntoa(clientAddr.sin_addr));
	return 0;
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
