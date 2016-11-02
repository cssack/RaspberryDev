#include "gpioManager.h"

#define TRANSLATORCOUNT 16

static const int translator[TRANSLATORCOUNT] = {0,1,2,3,4,5,6,7,21,22,23,24,25,26,27,28};

void gpio_init()
{
	wiringPiSetup ();
}

void gpio_setValues(uint16_t values, uint16_t mask)
{
	for (int i = 0; i< TRANSLATORCOUNT; i++)
	{
		int m = (mask >> i) & 1;
		if (m != 1)
			continue;
		int value = ((values >> i) & 1);

		pinMode(translator[i], OUTPUT);
		digitalWrite(translator[i], value);
	}
}

enum LedSet gpio_state()
{
	enum LedSet value = 0;
	
	for (int i = 0; i<TRANSLATORCOUNT; i++)
	{
		int wpi = translator[i];
		int val = digitalRead(wpi);

		value = value | (val << i);
	}
	return value;
}
