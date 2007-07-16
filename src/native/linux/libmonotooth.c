#include <errno.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <arpa/inet.h>
#include <unistd.h>
#include <bluetooth/bluetooth.h>
#include <bluetooth/hci.h>
#include <bluetooth/hci_lib.h>
#include <bluetooth/rfcomm.h>

int openbluetoothsocket(bdaddr_t* bdaddr, bdaddr_t* bdto, int port)
{
	int sk;
	struct sockaddr_rc laddr,raddr;
	memset(&laddr, 0, sizeof(raddr));
	laddr.rc_family = AF_BLUETOOTH;
	bacpy(&laddr.rc_bdaddr, bdaddr);
	laddr.rc_channel = 0;
	memset(&raddr, 0, sizeof(raddr));
	raddr.rc_family = AF_BLUETOOTH;
	bacpy(&raddr.rc_bdaddr, bdto);
	raddr.rc_channel = port;
	sk = socket(PF_BLUETOOTH, SOCK_STREAM, BTPROTO_RFCOMM);
	if (sk < 0)
	{
		return -1;
	}
	if (bind(sk, (struct sockaddr *)&laddr, sizeof(laddr)) < 0) {
		printf("Can't bind RFCOMM socket %s \n", batostr(&laddr.rc_bdaddr));
		close(sk);
		return -1;
	}
	printf("Socket is %d",sk);
	if (connect(sk,(struct sockaddr *)&raddr, sizeof(raddr)) < 0)
	{
		printf("Cant connect rfcomm socket! %s \n On channel %d \n", batostr(&raddr.rc_bdaddr),raddr.rc_channel);
		close(sk);
		return -1;
	}	
	
	return 0;
}

int closeBluetoothSocket(int *sockfd)
{
	if(close(*sockfd)<0)
	{
		return -1;
	}
	return 0;
}