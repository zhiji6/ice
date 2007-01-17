// **********************************************************************
//
// Copyright (c) 2003-2007 ZeroC, Inc. All rights reserved.
//
// This copy of Ice is licensed to you under the terms described in the
// ICE_LICENSE file included in this distribution.
//
// **********************************************************************

public class Server : Ice.Application
{
    public override int run(string[] args)
    {
        callbackOnInterrupt();

        Ice.ObjectAdapter adapter = communicator().createObjectAdapter("Hello");
	_workQueue = new WorkQueue();
        adapter.add(new HelloI(_workQueue), communicator().stringToIdentity("hello"));

	_workQueue.Start();
        adapter.activate();

        communicator().waitForShutdown();
        return 0;
    }

    public override void interruptCallback(int sig)
    {
        _workQueue.destroy();
	_workQueue.Join();

	try
	{
	    communicator().destroy();
	}
	catch(Ice.Exception ex)
	{
	    System.Console.Error.WriteLine(appName() + ": " + ex);
	}
	catch(System.Exception ex)
	{
	    System.Console.Error.WriteLine(appName() + ": unknown exception: " + ex);
	}
    }

    public static void Main(string[] args)
    {
        Server app = new Server();
        int status = app.main(args, "config.server");
	if(status != 0)
	{
	    System.Environment.Exit(status);
	}
    }

    private WorkQueue _workQueue;
}
