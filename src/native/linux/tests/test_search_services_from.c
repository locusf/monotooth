#include "../include/includes.h"
#include "../sdp/sdpfuncs.h"

int main()
{
	bdaddr_t* addr;
	const char* addrstr = "00:10:60:AF:28:CE";
	str2ba(addrstr,addr);
	uint32_t uuid = 0;
	service_record** rec = search_services_from_with_uuid(addr,uuid);
	free(rec);
	return 0;	
}

