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

void printBinary(void* value, size_t size)
{
	char* bytes = (char*)value;
	for (size_t byte = 0; byte < size; byte++)
	{
		for (int bit = 0; bit < 8; bit++)
		{
			fprintf(stdout, "%d", ((bytes[byte] >> bit) & 1));
		}
	}
	fprintf(stdout, "\n");
}
