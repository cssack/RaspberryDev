#include"protocol.h"
static int getTemperature();
static int getRuntime();
static int sendInit();

void prot_init()
{
	char buffer[1];
	if (conn_read(buffer, 1) == CONNERROR)
		return;
	
	if (buffer[0] == 0)
		sendInit();
}


static int sendInit()
{

	int runtime = getRuntime();
	int temp = getTemperature();
	enum LedSet state = gpio_state();

	fprintf(stdout, "\t---> STATE REQUESTED: TIME: %d, TEMP:%d, STATE: %d\n", runtime, temp, (int)state);

	if (conn_write((char*)(&temp), sizeof(int)) == CONNERROR)
		return CONNERROR;
	if (conn_write((char*)(&runtime), sizeof(int)) == CONNERROR)
		return CONNERROR;
	if (conn_write((char*)(&state), 2) == CONNERROR)
		return CONNERROR;
	return 0;
}

//45084

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
