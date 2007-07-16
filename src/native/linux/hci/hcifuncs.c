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

void get_device_name(inquiry_info* info, char* name)
{
	int dev_id,sock;
	//name = "";
	dev_id = hci_get_route(NULL);
	sock = hci_open_dev(dev_id);
	if(sock < 0 || dev_id < 0 )
	{
		perror("Socket error!");
		exit(1);
	}	
	char namefound[248] = { 0 };
	if (hci_read_remote_name(sock, &(info)->bdaddr, sizeof(namefound), 
	namefound, 0) < 0)
		strcpy(namefound,"[unknown]");
	close(sock);
	strcpy(name,namefound);
}

void read_local_device(char* name, bdaddr_t* addr)
{
	int dev_id = -1,ctl;
	dev_id = hci_get_route(NULL);
	struct hci_dev_info di;
	hci_devinfo(dev_id,&di);
	if(!(dev_id < 0 ))
	{
	int i;
	if ((ctl = socket(AF_BLUETOOTH, SOCK_RAW, BTPROTO_HCI)) < 0) {
		perror("Can't open HCI socket.");
		exit(1);
	}	
	if (ioctl(ctl, HCIGETDEVINFO, (void *) &di)) {
		perror("Can't get device info");
		exit(1);
	}	
	char namearr[249];				
	int dd = hci_open_dev(di.dev_id);
	hci_read_local_name(dd,sizeof(namearr),namearr,1000);
	bacpy(addr,&di.bdaddr);
	hci_close_dev(dd);		
	//printf("Name: %s",di.name);
	namearr[248] = '\0';	
	strcpy(name,namearr);
	close(ctl);
	}
}

