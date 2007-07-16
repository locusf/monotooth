#include "../include/includes.h"
#include <curses.h>
int main()
{
	const char* service_name = "Test service";
	const char* service_dsc = "Testing service, I tell you";
	const char* service_prov = "From Testing(INC)";
	int channel = 11;
	uint32_t service_uuid_int[] = { 0, 0, 0, 0xABCD };
	sdp_session_t* sess = register_service(service_name,service_dsc,service_prov,channel,service_uuid_int);
	getch();
	sdp_close(sess);
	return 0;
}
