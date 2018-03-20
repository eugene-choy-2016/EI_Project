/* 
 * Copyright (c) 2001-2006 TIBCO Software Inc.
 * All rights reserved.
 * For more information, please contact:
 * TIBCO Software Inc., Palo Alto, California, USA
 * 
 * $Id: tibjmsAsyncMsgConsumer.java 21731 2006-05-01 21:41:34Z $
 * 
 */

/*
 * This is a simple sample of a basic asynchronous
 * tibjmsMsgConsumer.
 *
 * This sampe subscribes to specified destination and
 * receives and prints all received messages.
 *
 * Notice that the specified destination should exist in your configuration
 * or your topics/queues configuration file should allow
 * creation of the specified destination.
 *
 * If this sample is used to receive messages published by
 * tibjmsMsgProducer sample, it must be started prior
 * to running the tibjmsMsgProducer sample.
 *
 * Usage:  java tibjmsAsyncMsgConsumer [options]
 *
 *    where options are:
 *
 *      -server     Server URL.
 *                  If not specified this sample assumes a
 *                  serverUrl of "tcp://localhost:7222"
 *
 *      -user       User name. Default is null.
 *      -password   User password. Default is null.
 *      -topic      Topic name. Default is "topic.sample"
 *      -queue      Queue name. No default
 *
 *
 */

import java.io.IOException;
import javax.jms.*;

public class tibjmsAsyncMsgConsumer
       implements ExceptionListener, MessageListener
{
    public static final int QUEUE = 1;
    public static final int TOPIC = 2;
    

    /*-----------------------------------------------------------------------
     * Parameters
     *----------------------------------------------------------------------*/

    private String      serverUrl;
    private String      userName;
    private String      password;
    private String      name;
    private int         messageType;

    /*-----------------------------------------------------------------------
     * Variables
     *----------------------------------------------------------------------*/
    Connection      connection;
    Session         session;
    MessageConsumer msgConsumer;
    Destination     destination;


    public tibjmsAsyncMsgConsumer(String serverUrl,String name,int messageType)
    {
        userName = "";
        password = "";
        
        this.serverUrl = serverUrl;
        this.name = name;
        this.messageType = messageType;

        try
        {
            tibjmsUtilities.initSSLParams(serverUrl,args);
        }
        catch(JMSSecurityException e)
        {
            System.err.println("JMSSecurityException: "+e.getMessage()+", provider="+e.getErrorCode());
            e.printStackTrace();
            System.exit(0);
        }

        try
        {
            int c;

            ConnectionFactory factory = new com.tibco.tibjms.TibjmsConnectionFactory(serverUrl);

            /* create the connection */
            connection = factory.createConnection(userName,password);

            /* create the session */
            session = connection.createSession(false,javax.jms.Session.AUTO_ACKNOWLEDGE);

            /* set the exception listener */
            connection.setExceptionListener(this);

            /* create the destination */
            if(messageType == TrainAsyncMsgConsumer.TOPIC)
                destination = session.createTopic(name);
            else
                destination = session.createQueue(name);

            System.err.println("Subscribing to destination: "+name);

            /* create the consumer */
            msgConsumer = session.createConsumer(destination);

            /* set the message listener */
            msgConsumer.setMessageListener(this);

            /* start the connection */
            connection.start();

            // Note: when message callback is used, the session
            // creates the dispatcher thread which is not a daemon
            // thread by default. Thus we can quit this method however
            // the application will keep running. It is possible to
            // specify that all session dispatchers are daemon threads.
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
    }

    /*---------------------------------------------------------------------
     * onException
     *---------------------------------------------------------------------*/
    public void onException(JMSException e)
    {
        /* print the connection exception status */
        System.err.println("CONNECTION EXCEPTION: "+ e.getMessage());
    }

    /*---------------------------------------------------------------------
     * onMessage
     *---------------------------------------------------------------------*/
    public void onMessage(Message msg)
    {
        try
        {
            System.err.println("Received message: " + msg);
        }
        catch(Exception e)
        {
            System.err.println("Unexpected exception in the message callback!");
            e.printStackTrace();
            System.exit(-1);
        }
    }



}


