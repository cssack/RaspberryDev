#include "server.h"
#include "connectionManager.h"


int main(int argc, char **argv)
{
	setbuf(stdout, NULL);
	while (1 == 1)
	{
		conn_waitForClient(12345);
		conn_close();
	}
}
