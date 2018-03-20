/* 
 * Copyright (c) 2001-$Date: 2006-06-20 10:23:46 -0700 (Tue, 20 Jun 2006) $ TIBCO Software Inc. 
 * All rights reserved.
 * For more information, please contact:
 * TIBCO Software Inc., Palo Alto, California, USA
 * 
 * $Id: csMsgProducer.cs 22233 2006-06-20 17:23:46Z bmahurka $
 * 
 */

/// <summary>
///  This is a sample of a basic csMsgProducer.
/// 
///  This samples publishes specified message(s) on a specified
///  destination and quits.
/// 
///  Notice that the specified destination should exist in your configuration
///  or your topics/queues configuration file should allow
///  creation of the specified topic or queue. Sample configuration supplied with
///  the TIBCO EMS allows creation of any destination.
/// 
///  If this sample is used to publish messages into
///  csMsgConsumer sample, the csMsgConsumer
///  sample must be started first.
/// 
///  If -topic is not specified this sample will use a topic named
///  "topic.sample".
/// 
///  Usage:  csMsgProducer  [options]
///                                <message-text1>
///                                ...
///                                <message-textN>
/// 
///   where options are:
/// 
///    -server    <server-url>  Server URL.
///                             If not specified this sample assumes a
///                             serverUrl of "tcp://localhost:7222"
///    -user      <user-name>   User name. Default is null.
///    -password  <password>    User password. Default is null.
///    -topic     <topic-name>  Topic name. Default value is "topic.sample"
///    -queue     <queue-name>  Queue name. No default
/// 
/// </summary>

using System;
using System.Collections;
using TIBCO.EMS;

public class csMsgProducer
{
    String     serverUrl = null;
    String     userName  = null;
    String     password  = null;
    String     name      = "topic.sample";
    ArrayList  data      = new ArrayList();
    bool       useTopic  = true;
	
    Connection       connection  = null;
    Session          session     = null;
    MessageProducer  msgProducer = null;
    Destination      destination = null;
	
    public csMsgProducer(String[] args) 
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
        
        Console.WriteLine("\n------------------------------------------------------------------------");
        Console.WriteLine("csMsgProducer SAMPLE");
        Console.WriteLine("------------------------------------------------------------------------");
        Console.WriteLine("Server....................... " + ((serverUrl != null)?serverUrl:"localhost"));
        Console.WriteLine("User......................... " + ((userName != null)?userName:"(null)"));
        Console.WriteLine("Destination.................. " + name);
        Console.WriteLine("Message Text................. ");

        for (int i = 0; i < data.Count; i++)
        {
            Console.WriteLine(data[i]);
        }
        Console.WriteLine("------------------------------------------------------------------------\n");
        
        try
        {
            BytesMessage msg;
            int i;
            
            if (data.Count == 0)
            {
                Console.Error.WriteLine("Error: must specify at least one message text\n");
                Usage();
            }
            
            Console.WriteLine("Publishing to destination '" + name + "'\n");
            
            ConnectionFactory factory = new TIBCO.EMS.ConnectionFactory(serverUrl);
            
            connection = factory.CreateConnection(userName, password);
			
            // create the session
            session = connection.CreateSession(false, Session.AUTO_ACKNOWLEDGE);
            
            // create the destination
            if (useTopic)
                destination = session.CreateTopic(name);
            else
                destination = session.CreateQueue(name);
            
            // create the producer
            msgProducer = session.CreateProducer(null);
            
            // publish messages
            for (i = 0; i < data.Count; i++)
            {
                // create text message
                //msg = session.CreateTextMessage();
                msg = session.CreateBytesMessage();
                
                // set message text
                //msg.Text = (String) data[i];
                msg.WriteBoolean(false);
                msg.WriteChar('a');
                msg.WriteShort(289);
                msg.WriteInt(System.Int32.MinValue);
                msg.WriteLong(10000111121);
                msg.WriteFloat((float)-2.23);
                msg.WriteDouble(-1.2345678);
                
                // publish message
                msgProducer.Send(destination, msg);
                
                Console.WriteLine("Published message: " + data[i]);
            }
            
            // close the connection
            connection.Close();
        }
        catch (EMSException e)
        {
            Console.Error.WriteLine("Exception in csMsgProducer: " + e.Message);
            Console.Error.WriteLine(e.StackTrace);
            Environment.Exit(-1);
        }
    }
    
    private void Usage() 
    {
        Console.WriteLine("\nUsage: csMsgProducer [options]");
        Console.WriteLine("                       <message-text-1>");
        Console.WriteLine("                       [<message-text-2>] ...\n");
        Console.WriteLine("");
        Console.WriteLine("   where options are:");
        Console.WriteLine("");
        Console.WriteLine("   -server   <server URL>  - Server URL, default is local server");
        Console.WriteLine("   -user     <user name>   - user name, default is null");
        Console.WriteLine("   -password <password>    - password, default is null");
        Console.WriteLine("   -topic    <topic-name>  - topic name, default is \"topic.sample\"");
        Console.WriteLine("   -queue    <queue-name>  - queue name, no default");
#if _NET_20
        Console.WriteLine("   -help-ssl               - help on ssl parameters");
#endif        
        Environment.Exit(0);
    }
    
    private void ParseArgs(String[] args) 
    {
        int i = 0;
        
        while (i < args.Length)
        {
            if (args[i].CompareTo("-server") == 0) 
            {
                if ((i + 1) >= args.Length)
                    Usage();
                serverUrl = args[i + 1];
                i += 2;
            } 
            else 
            if (args[i].CompareTo("-topic") == 0) 
            {
                if ((i + 1) >= args.Length)
                    Usage();
                name = args[i + 1];
                i += 2;
            } 
            else 
            if (args[i].CompareTo("-queue") == 0) 
            {
                if ((i + 1) >= args.Length)
                    Usage();
                name = args[i + 1];
                i += 2;
                useTopic = false;
            } 
            else 
            if (args[i].CompareTo("-user") == 0) 
            {
                if ((i + 1) >= args.Length)
                    Usage();
                userName = args[i + 1];
                i += 2;
            } 
            else 
            if (args[i].CompareTo("-password") == 0) 
            {
                if ((i + 1) >= args.Length)
                    Usage();
                password = args[i + 1];
                i += 2;
            } 
            else 
            if (args[i].CompareTo("-help") == 0) 
            {
                Usage();
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
    
//    public static void Main(String[] args) 
//    {
//         csMsgProducer t = new csMsgProducer(args);
//    }
}
