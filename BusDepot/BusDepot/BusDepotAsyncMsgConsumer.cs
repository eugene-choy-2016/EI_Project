/// <summary>
/// This is a simple sample of a basic asynchronous csMsgConsumer.
///
/// This sample subscribes to specified destination and receives and prints all
/// received messages. 
///
/// Notice that the specified destination should exist in your configuration
/// or your topics/queues configuration file should allow creation of the
/// specified destination.
///
/// If this sample is used to receive messages published by csMsgProducer
/// sample, it must be started prior to running the csMsgProducer sample.
///
/// Usage:  csAsyncMsgConsumer [options]
///
///    where options are:
///
///    -server    <server-url/>  Server URL.
///                             If not specified this sample assumes a
///                             serverUrl of "tcp://localhost:7222"
///    -user      <user-name/>   User name. Default is null.
///    -password  <password/>    User password. Default is null.
///    -topic     <topic-name/>  Topic name. Default value is "topic.sample"
///    -queue     <queue-name/>  Queue name. No default
///
/// </summary>

using System;
using System.Xml;
using TIBCO.EMS;

public class BusDepotAsyncMsgConsumer : IExceptionListener, IMessageListener 
{
    String  serverUrl  = null;
    String  userName   = null;
    String  password   = null;
    String  name       = "topic.sample";
    bool    useTopic   = true;

    Connection       connection  = null;
    Session          session     = null;
    MessageConsumer  msgConsumer = null;
    Destination      destination = null; 

    public BusDepotAsyncMsgConsumer(String[] args) 
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
        Console.WriteLine("------------------------------------------------------------------------");
        Console.WriteLine("csAsyncMsgConsumer SAMPLE");
        Console.WriteLine("------------------------------------------------------------------------");
        Console.WriteLine("Server....................... " + ((serverUrl != null)?serverUrl:"localhost"));
        Console.WriteLine("User......................... " + ((userName != null)?userName:"(null)"));
        Console.WriteLine("Destination.................. " + name);
        Console.WriteLine("------------------------------------------------------------------------\n");

        try {
            ConnectionFactory factory = new TIBCO.EMS.ConnectionFactory(serverUrl);
            connection = factory.CreateConnection(userName, password);
            session = connection.CreateSession(false, Session.AUTO_ACKNOWLEDGE);
            connection.ExceptionListener = this;

            if (useTopic)
                destination = session.CreateTopic(name);
            else
                destination = session.CreateQueue(name);

            Console.WriteLine("Subscribing to destination: " + name);

            // create the consumer
            msgConsumer = session.CreateConsumer(destination);
            msgConsumer.MessageListener = this;
            connection.Start();
        } 
        catch (Exception e) 
        {
            Console.Error.WriteLine("Exception in AsyncMsgConsumer: " +
                                    e.Message);
            Console.Error.WriteLine(e.StackTrace);
        }
    }

    private void  Usage() 
    {
        Console.WriteLine("\nUsage: csAsyncMsgConsumer [options]");
        Console.WriteLine("");
        Console.WriteLine("   where options are:");
        Console.WriteLine("");
        Console.WriteLine("   -server   <server URL> - Server URL, default is local server");
        Console.WriteLine("   -user     <user name>  - user name, default is null");
        Console.WriteLine("   -password <password>   - password, default is null");
        Console.WriteLine("   -topic    <topic-name> - topic name, default is \"topic.sample\"");
        Console.WriteLine("   -queue    <queue-name> - queue name, no default");
#if _NET_20
        Console.WriteLine("   -help-ssl              - help on ssl parameters");
#endif        
        Environment.Exit(0);
    }

    private void  ParseArgs(String[] args) 
    {
        int i = 0;

        while (i < args.Length) {
            if (args[i].CompareTo("-server") == 0) 
            {
                if ((i + 1) >= args.Length)
                    Usage();
                serverUrl = args[i+1];
                i += 2;
            } 
            else 
            if (args[i].CompareTo("-topic") == 0) 
            {
                if ((i + 1) >= args.Length)
                    Usage();
                name = args[i+1];
                i += 2;
            } 
            else 
            if (args[i].CompareTo("-queue") == 0) 
            {
                if ((i + 1) >= args.Length)
                    Usage();
                name = args[i+1];
                i += 2;
                useTopic = false;
            } 
            else 
            if (args[i].CompareTo("-user") == 0) 
            {
                if ((i + 1) >= args.Length)
                    Usage();
                userName = args[i+1];
                i += 2;
            } 
            else 
            if (args[i].CompareTo("-password") == 0) 
            {
                if ((i + 1) >= args.Length)
                    Usage();
                password = args[i+1];
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
                Console.Error.WriteLine("Unrecognized parameter: " + args[i]);
                Usage();
            }
        }
    }

    public void OnException(EMSException e) 
    {
        // print the connection exception status
        Console.Error.WriteLine("Connection Exception: " + e.Message);
    }

    public void OnMessage(Message msg)  {
		try
		{
			TextMessage textMsg = (TextMessage)msg;
			Console.WriteLine("Received message: " + textMsg.Text);
			if (textMsg.Text.Contains("Stop deploying bus")) {
				Console.WriteLine(name + ": Buses deployment has been stopped");
			} else {
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(textMsg.Text);
				string xPathString = "//busRequest/station";
				XmlNode station = doc.DocumentElement.SelectSingleNode(xPathString);
				Console.WriteLine(name + ": Buses have been dispatched to {0}", station.InnerText);
			}
		}
		catch (Exception e)
		{
			Console.Error.WriteLine("Unexpected exception message callback!");
			Console.Error.WriteLine(e.StackTrace);
			Environment.Exit(-1);
		}
		
		try {
            QueueConnectionFactory factory = new TIBCO.EMS.QueueConnectionFactory(serverUrl);
            QueueConnection connection = factory.CreateQueueConnection(userName, password);
            QueueSession session = connection.CreateQueueSession(false, Session.AUTO_ACKNOWLEDGE);

            /*
             * Use createQueue() to enable sending into dynamic queues.
             */
			String queueName = "q.deployed";
            TIBCO.EMS.Queue queue = session.CreateQueue(queueName);
            QueueSender sender = session.CreateSender(queue);

            /* publish messages */
			// TextMessage message = session.CreateTextMessage();
			// message.Text = "yolo";
			// sender.Send(message);
			// Console.WriteLine("Sent message: "+text);

            connection.Close();
        } catch (EMSException e) {
            Console.Error.WriteLine(e.StackTrace);
            Environment.Exit(0);
        }
    }
}
