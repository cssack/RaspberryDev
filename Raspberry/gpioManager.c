#include "gpioManager.h"



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
