#include "gpioManager.h"

#define TRANSLATORCOUNT 16

static const int translator[TRANSLATORCOUNT] = {7, 0, 1, 2, 3, 4, 5, 6, 21, 22, 26, 23, 24, 27, 25, 28};

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
