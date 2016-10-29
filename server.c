#include "server.h"
#include "connectionManager.h"
#include "gpioManager.h"


int main(int argc, char **argv)
{
	setbuf(stdout, NULL);
	gpio_init();
	uint8_t rcvBuffer[10];
	while (1==1)
	{
		conn_waitForClient(12345);
		if (conn_read(rcvBuffer, 10) == EXIT_FAILURE)
			continue;

		gpio_power(rcvBuffer[0]);
		conn_close();
	}
}
