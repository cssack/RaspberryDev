#include"protocol.h"
static int getTemperature();
static int getRuntime();
static int sendInit();
static int applyGpios();
static void printBinary(void* value, size_t size);

void prot_init()
{
	char buffer[1];
	if (conn_read(buffer, 1) == CONNERROR)
		return;
	
	if (buffer[0] == 0)
		sendInit();
	else if (buffer[0] == 1)
		applyGpios();

}


static int sendInit()
{
	int runtime = getRuntime();
	int temp = getTemperature();
	enum LedSet state = gpio_state();

	fprintf(stdout, "\t---> SEND STATE: TIME: %d, TEMP:%d, STATE: [", runtime, temp);
	printBinary(&state, 2);
	fprintf(stdout, "]\n");

	if (conn_write((char*)(&temp), sizeof(int)) == CONNERROR)
		return CONNERROR;
	if (conn_write((char*)(&runtime), sizeof(int)) == CONNERROR)
		return CONNERROR;
	if (conn_write((char*)(&state), 2) == CONNERROR)
		return CONNERROR;
	return 0;
}

static int applyGpios()
{
	char buffer[4];
	if (conn_read(buffer, 4) == CONNERROR)
		return CONNERROR;
	
	uint16_t values = buffer[1] << 8 | buffer[0];
	uint16_t mask = buffer[3] << 8 | buffer[2];

	fprintf(stdout, "\t---> APPLY GPIOS:\n");
	fprintf(stdout, "\t--->\tVALUES [");
	printBinary(&values, 2);
	fprintf(stdout, "]\n");
	fprintf(stdout, "\t--->\tMASK   [");
	printBinary(&mask, 2);
	fprintf(stdout, "]\n");

	gpio_setValues(values, mask);
	sendInit();
	return 0;
}


static int getTemperature()
{
	int value;
	FILE* file = fopen("/sys/class/thermal/thermal_zone0/temp", "r");
	if (file == NULL)
		return 0;

	fscanf (file, "%d", &value);
	fclose(file);

	return value;
}

static int getRuntime()
{
	double value;
	FILE* file = fopen("/proc/uptime", "r");
	if (file == NULL)
		return 0;

	fscanf (file, "%lf", &value);
	fclose(file);

	return (int)(value * 1000);
}

static void printBinary(void* value, size_t size)
{
	char* bytes = (char*)value;
	for (size_t byte = 0; byte < size; byte++)
	{
		for (int bit = 0; bit < 8; bit++)
		{
			fprintf(stdout, "%d", ((bytes[byte] >> bit) & 1));
		}
	}
}
