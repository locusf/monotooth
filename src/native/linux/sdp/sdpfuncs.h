#include "../include/includes.h"

typedef struct 
{
	int rfcomm_port;	
	char name[256];
	char description[256];	
} __attribute__ ((packed)) service_record;
