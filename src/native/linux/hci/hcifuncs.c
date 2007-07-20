#include "hcifuncs.h"

inquiry_info** inquire_devices()
{
	// Use the default bluetooth device, if any
	int dev_id = hci_get_route(NULL);
	inquiry_info *info = NULL,**infos = NULL;
	if(!(dev_id<0))
	{
	uint8_t lap[3] = { 0x33, 0x8b, 0x9e };
	int num_rsp, length, flags,max_rsp;	
	int i;
	length  = 8;	/* ~10 seconds */
	max_rsp = 255;
	flags   = IREQ_CACHE_FLUSH;
	num_rsp = hci_inquiry(dev_id, length, max_rsp, lap, &info, flags);
	if (num_rsp > 0)
	{
	infos = (inquiry_info**) calloc(num_rsp,sizeof(inquiry_info*));
	for (i = 0; i<num_rsp;i++)
	{
		infos[i] = (info+i);
	}
	}
	}
	return infos;
}
