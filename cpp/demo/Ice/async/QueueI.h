// **********************************************************************
//
// Copyright (c) 2003-2006 ZeroC, Inc. All rights reserved.
//
// This copy of Ice is licensed to you under the terms described in the
// ICE_LICENSE file included in this distribution.
//
// **********************************************************************

#ifndef QUEUE_I_H
#define QUEUE_I_H

#include <IceUtil/Mutex.h>
#include <Queue.h>
#include <list>

class QueueI : virtual public Demo::Queue, public IceUtil::Mutex
{
public:

    virtual void get_async(const Demo::AMD_Queue_getPtr&, const std::string&, const Ice::Current&);
    virtual void add(const std::string&, const Ice::Current&);
    virtual void cancel_async(const Demo::AMD_Queue_cancelPtr&, const std::vector<std::string>&, const Ice::Current&);

private:

    struct Request
    {
        std::string id;
	Demo::AMD_Queue_getPtr request;
    };

    std::list<std::string> _messageQueue;
    std::list<Request> _requestQueue;
};


#endif
