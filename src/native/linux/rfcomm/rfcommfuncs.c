#include "rfcommfuncs.h"

int rfcomm_connect(bdaddr_t* from, bdaddr_t* to, int channel)
{	
	int sk;
	struct sockaddr_rc laddr,raddr;
	memset(&laddr, 0, sizeof(raddr));
	laddr.rc_family = AF_BLUETOOTH;
	bacpy(&laddr.rc_bdaddr, from);
	laddr.rc_channel = 0;
	memset(&raddr, 0, sizeof(raddr));
	raddr.rc_family = AF_BLUETOOTH;
	bacpy(&raddr.rc_bdaddr, to);
	raddr.rc_channel = channel;
	sk = socket(AF_BLUETOOTH, SOCK_STREAM, BTPROTO_RFCOMM);
	if (sk < 0)
	{
		return -1;
	}
	if (bind(sk, (struct sockaddr *)&laddr, sizeof(laddr)) < 0) {
		printf("Can't bind RFCOMM socket %s \n", batostr(&laddr.rc_bdaddr));
		close(sk);
		return -1;
	}	
	if (connect(sk,(struct sockaddr *)&raddr, sizeof(raddr)) < 0)
	{
		printf("Cant connect rfcomm socket! %s \n On channel %d \n", batostr(&raddr.rc_bdaddr),raddr.rc_channel);
		close(sk);
		return -1;
	}	
	
	return sk;
}

int rfcomm_listen(bdaddr_t* from, int channel, int conns)
{
	int sk;
	struct sockaddr_rc laddr;
	struct sockaddr_rc raddr = {0};
	socklen_t len;		
	memset(&laddr, 0, sizeof(laddr));
	//memset(&raddr, 0, sizeof(raddr));
	laddr.rc_family = AF_BLUETOOTH;
	laddr.rc_bdaddr = *BDADDR_ANY;
	laddr.rc_channel = channel;
	sk = socket(AF_BLUETOOTH, SOCK_STREAM, BTPROTO_RFCOMM);
	if (sk < 0)
	{
		return -1;
	}
	if (bind(sk, (struct sockaddr *)&laddr, sizeof(laddr)) < 0) {
		printf("Can't bind RFCOMM socket %s \n", batostr(&laddr.rc_bdaddr));
		close(sk);
		return -1;
	}
	listen(sk,conns);
	len = sizeof(raddr);
	int sok = accept(sk, (struct sockaddr*)&raddr, &len);
	return sok;	
}

int customread(int fd, char* buf, int off, int len)
{
    int done = 0;
 
   while(done < len) {
       int count = read (fd, buf+off+done, len-done);
 
        if (count < 0) {            
            perror("Failed to read");
            return -1;
        }
     done += count;
    }
    return done;
}

int customwrite(int fd, char* buf, int off, int len)
{
    int done = 0;
 
   while(done < len) {
       int count = write (fd, buf+off+done, len-done);
 
        if (count < 0) {            
            perror("Failed to write");
            return -1;
        }
     done += count;
    }
    return done;
}
