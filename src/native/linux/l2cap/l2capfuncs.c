#include "l2capfuncs.h"

int l2cap_connect(bdaddr_t* to, unsigned short psm)
{
	struct sockaddr_l2 addr = { 0 };
    	int s, status;
    	char *message = "hello!";
    	// allocate a socket
    	s = socket(AF_BLUETOOTH, SOCK_SEQPACKET, BTPROTO_L2CAP);

    	// set the connection parameters (who to connect to)
    	addr.l2_family = AF_BLUETOOTH;
    	addr.l2_psm = htobs(psm);
    	bacpy( &addr.l2_bdaddr, to );

    	// connect to server
    	status = connect(s, (struct sockaddr *)&addr, sizeof(addr));

    	// send a message
    	if( status == 0 ) {
	        status = write(s, "hello!", 6);
    	}	

	    if( status < 0 ) perror("uh oh");

}

int l2cap_listen(unsigned short psm, int maxconns)
{
	struct sockaddr_l2 loc_addr = { 0 }, rem_addr = { 0 };
	char buf[1024] = { 0 };
    	int s, client, bytes_read;
    	int opt = sizeof(rem_addr);

    	// allocate socket
    	s = socket(AF_BLUETOOTH, SOCK_SEQPACKET, BTPROTO_L2CAP);

    	// bind socket to port 0x1001 of the first available 
    	// bluetooth adapter
    	loc_addr.l2_family = AF_BLUETOOTH;
    	loc_addr.l2_bdaddr = *BDADDR_ANY;
    	loc_addr.l2_psm = htobs(psm);

    	bind(s, (struct sockaddr *)&loc_addr, sizeof(loc_addr));

    	// put socket into listening mode
    	listen(s, maxconns);
    	
    	client = accept(s, (struct sockaddr *)&rem_addr, &opt);   
    	return client;
}
