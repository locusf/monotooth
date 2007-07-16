#include "sdpfuncs.h"

service_record** search_services_from(bdaddr_t* target)
{	
    	uuid_t svc_uuid;
    	int err,i=0;    	
    	sdp_list_t *response_list = NULL, *search_list, *attrid_list;
    	sdp_session_t *session = 0;
	    service_record** services = NULL;
    	// connect to the SDP server running on the remote machine
    	session = sdp_connect( BDADDR_ANY, target, SDP_RETRY_IF_BUSY );

    	// specify the UUID of all services
    	sdp_uuid16_create( &svc_uuid, PUBLIC_BROWSE_GROUP );
    	search_list = sdp_list_append( NULL, &svc_uuid );

    	// specify that we want a list of all the matching applications' attributes
    	uint32_t range = 0x0000ffff;
    	attrid_list = sdp_list_append( NULL, &range );
    	
    	err = sdp_service_search_attr_req( session, search_list, \
        	SDP_ATTR_REQ_RANGE, attrid_list, &response_list);
	sdp_list_t *r = response_list;
	if(sdp_list_len(r)>0)
	{
	services = (service_record**) calloc(sdp_list_len(r),sizeof(service_record*));	
    	// go through each of the service records
    	for (; r; r = r->next ) 
	{
        sdp_record_t *rec = (sdp_record_t*) r->data;
        sdp_list_t *proto_list;
        sdp_data_t *d = sdp_data_get(rec, SDP_ATTR_SVCNAME_PRIMARY);		
	services[i] = (service_record*)malloc(sizeof(service_record));
	if(d)
	{
		char servname[256];
		strcpy(servname,d->val.str);		
		//services[i]->name = malloc(256);
		strcpy(services[i]->name,servname);
	}
	d = sdp_data_get(rec,SDP_ATTR_SVCDESC_PRIMARY);
	if(d)
	{		
		char servdescr[256];
		strcpy(servdescr,d->val.str);
		//services[i]->description = malloc(256);
		strcpy(services[i]->description,servdescr);
	}
        // get a list of the protocol sequences
        if( sdp_get_access_protos( rec, &proto_list ) == 0 ) {
        sdp_list_t *p = proto_list;

        // go through each protocol sequence
        for( ; p ; p = p->next ) {
            sdp_list_t *pds = (sdp_list_t*)p->data;

            // go through each protocol list of the protocol sequence
            for( ; pds ; pds = pds->next ) {

                // check the protocol attributes
                sdp_data_t *d = (sdp_data_t*)pds->data;
                int proto = 0;
                for( ; d; d = d->next ) {
                    switch( d->dtd ) { 
                        case SDP_UUID16:
                        case SDP_UUID32:
                        case SDP_UUID128:
                            proto = sdp_uuid_to_proto( &d->val.uuid );
                            break;
                        case SDP_UINT8:
                            if( proto == RFCOMM_UUID ) {
				services[i]->rfcomm_port = (int) malloc(sizeof(int));
				services[i]->rfcomm_port = d->val.int8;                    
                            }			    
                            break;
                    }
                }
            }
            sdp_list_free( (sdp_list_t*)p->data, 0 );
        }
        sdp_list_free( proto_list, 0 );

        }        
        sdp_record_free( rec );	
	i++;
    	}
	}
    sdp_close(session);
	return services;
}

service_record** search_services_from_with_uuid(bdaddr_t* target, uint32_t uuid)
{   
        uuid_t svc_uuid;
        int err,i=0;        
        sdp_list_t *response_list = NULL, *search_list, *attrid_list;
        sdp_session_t *session = 0;
        service_record** services = NULL;
        // connect to the SDP server running on the remote machine
        session = sdp_connect( BDADDR_ANY, target, SDP_RETRY_IF_BUSY );        
        // specify the UUID of a service
        sdp_uuid32_create( &svc_uuid, uuid );
        search_list = sdp_list_append( NULL, &svc_uuid );

        // specify that we want a list of all the matching applications' attributes
        uint32_t range = 0x0000ffff;
        attrid_list = sdp_list_append( NULL, &range );
        
        err = sdp_service_search_attr_req( session, search_list, \
            SDP_ATTR_REQ_RANGE, attrid_list, &response_list);
    sdp_list_t *r = response_list;    
    services = (service_record**) calloc(sdp_list_len(r),sizeof(service_record*));  
        // go through each of the service records
        for (; r; r = r->next ) 
    {
        sdp_record_t *rec = (sdp_record_t*) r->data;
        sdp_list_t *proto_list;
        sdp_data_t *d = sdp_data_get(rec, SDP_ATTR_SVCNAME_PRIMARY);        
    services[i] = (service_record*)malloc(sizeof(service_record));
    if(d)
    {
        char servname[256];
        strcpy(servname,d->val.str);        
        //services[i]->name = malloc(256);
        strcpy(services[i]->name,servname);
    }
    d = sdp_data_get(rec,SDP_ATTR_SVCDESC_PRIMARY);
    if(d)
    {       
        char servdescr[256];
        strcpy(servdescr,d->val.str);
        //services[i]->description = malloc(256);
        strcpy(services[i]->description,servdescr);
    }
        // get a list of the protocol sequences
        if( sdp_get_access_protos( rec, &proto_list ) == 0 ) {
        sdp_list_t *p = proto_list;

        // go through each protocol sequence
        for( ; p ; p = p->next ) {
            sdp_list_t *pds = (sdp_list_t*)p->data;

            // go through each protocol list of the protocol sequence
            for( ; pds ; pds = pds->next ) {

                // check the protocol attributes
                sdp_data_t *d = (sdp_data_t*)pds->data;
                int proto = 0;
                for( ; d; d = d->next ) {
                    switch( d->dtd ) { 
                        case SDP_UUID16:
                        case SDP_UUID32:
                        case SDP_UUID128:
                            proto = sdp_uuid_to_proto( &d->val.uuid );
                            break;
                        case SDP_UINT8:
                            if( proto == RFCOMM_UUID ) {
                services[i]->rfcomm_port = (int) malloc(sizeof(int));
                services[i]->rfcomm_port = d->val.int8;                    
                            }               
                            break;
                    }
                }
            }
            sdp_list_free( (sdp_list_t*)p->data, 0 );
        }
        sdp_list_free( proto_list, 0 );

        }        
        sdp_record_free( rec ); 
    i++;
        }
    
    sdp_close(session);
    return services;
}

int count_number_of_records(service_record* recs)
{
	return (sizeof(recs)/sizeof(service_record));
}


sdp_session_t* register_service(const char* service_name, const char* service_dsc,const char* service_prov, int rfcomm_channel, uint32_t service_uuid_int)
{
    uuid_t root_uuid, l2cap_uuid, rfcomm_uuid, svc_uuid, svc_uuid2;
    sdp_list_t *l2cap_list = 0,
               *classid_list = 0, 
               *rfcomm_list = 0,
               *root_list = 0,
               *proto_list = 0, 
               *access_proto_list = 0;
    sdp_data_t *channel = 0, *psm = 0;
    sdp_record_t *record = sdp_record_alloc();
    // set the general service ID
    sdp_uuid16_create( &svc_uuid, SERIAL_PORT_SVCLASS_ID);    
    sdp_set_service_id( record, svc_uuid );
    classid_list = sdp_list_append(0,&svc_uuid);
    sdp_uuid32_create(&svc_uuid2, service_uuid_int);
    sdp_list_append(classid_list,&svc_uuid2);
    sdp_set_service_classes(record,classid_list);
    // make the service record publicly browsable
    sdp_uuid16_create(&root_uuid, PUBLIC_BROWSE_GROUP);
    root_list = sdp_list_append(0, &root_uuid);
    sdp_set_browse_groups( record, root_list );

    // set l2cap information
    sdp_uuid16_create(&l2cap_uuid, L2CAP_UUID);
    l2cap_list = sdp_list_append( 0, &l2cap_uuid );
    proto_list = sdp_list_append( 0, l2cap_list );

    // set rfcomm information
    sdp_uuid16_create(&rfcomm_uuid, RFCOMM_UUID);
    channel = sdp_data_alloc(SDP_UINT8, &rfcomm_channel);
    rfcomm_list = sdp_list_append( 0, &rfcomm_uuid );
    sdp_list_append( rfcomm_list, channel );
    sdp_list_append( proto_list, rfcomm_list );

    // attach protocol information to service record
    access_proto_list = sdp_list_append( 0, proto_list );
    sdp_set_access_protos( record, access_proto_list );
    
    // set the name, provider, and description
    sdp_set_info_attr(record, service_name, service_prov, service_dsc);
    int err = 0;
    sdp_session_t *session = 0;    
    // connect to the local SDP server, register the service record, and 
    // disconnect
    session = sdp_connect( BDADDR_ANY, BDADDR_LOCAL, SDP_RETRY_IF_BUSY );
    err = sdp_record_register(session, record, 0);

    // cleanup
    sdp_data_free( channel );
    sdp_list_free( l2cap_list, 0 );
    sdp_list_free( rfcomm_list, 0 );
    sdp_list_free( root_list, 0 );
    sdp_list_free( access_proto_list, 0 );
    return session;     
}
