#include "server.h"
#include "connectionManager.h"
#include "gpioManager.h"
#include "protocol.h"

void printBinary(void* value, size_t size);

int main(int argc, char **argv)
{
	setbuf(stdout, NULL);
	gpio_init();


	while (1==1)
	{
		conn_waitForClient(12345);

		prot_init();

		conn_close();
	}
}

