// monotooth.h


#include <winsock2.h>
#include <Ws2bth.h>
#include <BluetoothAPIs.h>

#pragma comment (lib, "irprops.lib")

typedef struct
	{
		BYTE addr[6];
		char* friendlyname;
	} FoundDevice;
__declspec(dllexport) FoundDevice** inquire();
