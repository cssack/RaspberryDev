#include "gpioManager.h"

#define TRANSLATORCOUNT 16

static const int translator[TRANSLATORCOUNT] = {0,1,2,3,4,5,6,7,21,22,23,24,25,26,27,28};

static int toWpiFromLed(enum LedSet ledNumber);
static enum LedSet toLedFromWpi(int wpi);

void gpio_init()
{
	wiringPiSetup ();
	pinMode(LED, OUTPUT);
}

void gpio_power(uint8_t power)
{
	if (power != 0)
		digitalWrite(LED, HIGH);
	else
		digitalWrite(LED, LOW);
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

static int toWpiFromLed(enum LedSet ledNumber)
{
	int indexer = 0;
	while ((ledNumber & 1) != 1)
	{
		ledNumber = ledNumber >> 1;
		indexer++;
	}
	return translator[indexer];
}

static enum LedSet toLedFromWpi(int wpi)
{
	for (int i = 0; i<TRANSLATORCOUNT; i++)
	{
		if (wpi == translator[i])
			return 1 << i;
	}
	return 0;
}
