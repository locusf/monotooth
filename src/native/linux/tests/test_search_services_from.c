#include "../include/includes.h"
#include "../sdp/sdpfuncs.h"

int main()
{
	bdaddr_t *addr;
	const char* addrstr = "00:10:60:AF:28:CE";
	str2ba(addrstr,addr);
	printf("%d\n",sizeof(service_record));
	uint32_t uuid[] = {0x0,0x0,0x0,0xABCD};
	search_services_from_with_uuid(addr,uuid);
	
}

