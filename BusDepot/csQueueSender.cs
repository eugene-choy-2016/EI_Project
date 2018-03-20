/* 
 * Copyright (c) 2001-$Date: 2006-06-20 10:23:46 -0700 (Tue, 20 Jun 2006) $ TIBCO Software Inc. 
 * All rights reserved.
 * For more information, please contact:
 * TIBCO Software Inc., Palo Alto, California, USA
 * 
 * $Id: csQueueSender.cs 22233 2006-06-20 17:23:46Z bmahurka $
 * 
 */

/// <summary>
/// This is a testing of basic QueueSender.
///
/// This test sends specified message(s) into specified
/// queue and quits.
///
/// Notice that specified queue should exist in your configuration
/// or your queues configuration file should allow creation of the
/// specified queue.
///
/// This test can send into dynamic queues thus it is
/// using the QueueSession.CreateQueue() method
/// to obtain the Queue object.
///
/// To test on secure queues, please make sure the username/password
/// used have the granted permission on the queue.
///
/// Usage:  csQueueSender  [options]
///                                  message-text1
///                                  ...
///                                  message-textN
///
///
///    where options are:
///
///    -server    <server-url>  Server URL.
///                             If not specified this sample assumes a
///                             serverUrl of "tcp://localhost:7222"
///    -user      <user-name>   User name. Default is null.
///    -password  <password>    User password. Default is null.
///    -queue     <queue-name>  Queue name. Default is "queue.sample".
///
/// </summary>

using System;
using System.Collections;
using TIBCO.EMS;

public class csQueueSender {
    string     serverUrl       = null;
    string     userName        = null;
    string     password        = null;

    string     queueName       = "queue.sample";

    ArrayList  data            = new ArrayList();

    internal csQueueSender(string[] args) 
    {
        ParseArgs(args);

#if _NET_20
        try {
            tibemsUtilities.initSSLParams(serverUrl,args);
        }
        catch (Exception e)
        {
            System.Console.WriteLine("Exception: "+e.Message);
            System.Console.WriteLine(e.StackTrace);
            System.Environment.Exit(-1);
        }
#endif        
        if (data.Count == 0) {
            Console.Error.WriteLine("Error: must specify at least one message text");
            usage();
        }

        Console.WriteLine("\n------------------------------------------------------------------------");
        Console.WriteLine("csQueueSender SAMPLE");
        Console.WriteLine("------------------------------------------------------------------------");
        Console.WriteLine("Server....................... " + ((serverUrl != null)?serverUrl:"localhost"));
        Console.WriteLine("User......................... " + ((userName != null)?userName:"(null)"));
        Console.WriteLine("Queue........................ " + queueName);
        Console.WriteLine("------------------------------------------------------------------------\n");

        try {
            QueueConnectionFactory factory = new TIBCO.EMS.QueueConnectionFactory(serverUrl);

            QueueConnection connection = factory.CreateQueueConnection(userName, password);

            QueueSession session = connection.CreateQueueSession(false, Session.AUTO_ACKNOWLEDGE);

            /*
             * Use createQueue() to enable sending into dynamic queues.
             */
            TIBCO.EMS.Queue queue = session.CreateQueue(queueName);

            QueueSender sender = session.CreateSender(queue);

            /* publish messages */
            for (int i=0; i<data.Count; i++) {
                TextMessage message = session.CreateTextMessage();
                string text = (string)data[i];
                message.Text = text;
                sender.Send(message);
                Console.WriteLine("Sent message: "+text);
            }

            connection.Close();
        } catch (EMSException e) {
            Console.Error.WriteLine(e.StackTrace);
            Environment.Exit(0);
        }
    }

    public static void Main(string[] args)
    {
        csQueueSender t = new csQueueSender(args);
    }

    void usage()
    {
        Console.WriteLine("\nUsage: csQueueSender [options]");
        Console.WriteLine("                                <message-text1 ... message-textN>");
        Console.WriteLine("");
        Console.WriteLine("   where options are:");
        Console.WriteLine("");
        Console.WriteLine(" -server    <server URL> - Server URL, default is tcp://localhost:7222");
        Console.WriteLine(" -user      <user name>  - user name, default is null");
        Console.WriteLine(" -password  <password>   - password, default is null");
        Console.WriteLine(" -queue     <queue-name> - queue name, default is \"queue.sample\"");
#if _NET_20
        Console.WriteLine(" -help-ssl               - help on ssl parameters");
#endif        
        Environment.Exit(0);
    }

    void ParseArgs(string[] args)
    {
        int i=0;

        while (i < args.Length) 
        {
            if (args[i].Equals("-server")) 
            {
                if ((i+1) >= args.Length) {
                    usage();
                }
                serverUrl = args[i+1];
                i += 2;
            }
            else 
            if (args[i].Equals("-queue")) 
            {
                if ((i+1) >= args.Length) {
                    usage();
                }
                queueName = args[i+1];
                i += 2;
            }
            else 
            if (args[i].Equals("-user")) 
            {
                if ((i+1) >= args.Length) {
                    usage();
                }
                userName = args[i+1];
                i += 2;
            }
            else 
            if (args[i].Equals("-password")) 
            {
                if ((i+1) >= args.Length) {
                    usage();
                }
                password = args[i+1];
                i += 2;
            }
            else 
            if (args[i].Equals("-help")) 
            {
                usage();
            }
#if _NET_20
            else 
            if (args[i].CompareTo("-help-ssl")==0)
            {
                tibemsUtilities.sslUsage();
            }
            else
            if(args[i].StartsWith("-ssl"))
            {
                i += 2;
            }         
#endif
            else 
            {
                data.Add(args[i]);
                i++;
            }
        }
    }
}


