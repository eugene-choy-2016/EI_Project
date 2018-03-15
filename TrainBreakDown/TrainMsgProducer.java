import java.util.*;
import javax.jms.*;

public class TrainMsgProducer{

    public static final int QUEUE = 1;
    public static final int TOPIC = 2;

    private  String          serverUrl;
    private  String          name;
    private  int             producerType;
    
    private  Connection      connection;
    private  Session         session;
    private  MessageProducer msgProducer;
    private  Destination     destination;

    public TrainMsgProducer(String serverUrl,String name,int producerType){
        this.serverUrl = serverUrl;
        this.name = name;
        this.producerType = producerType;
        try {
            tibjmsUtilities.initSSLParams(serverUrl,new String[0]);
        }
        catch (JMSSecurityException e)
        {
            System.err.println("JMSSecurityException: "+e.getMessage()+", provider="+e.getErrorCode());
            e.printStackTrace();
            System.exit(0);
        }
    }
    
    public void sendMessage(String message){
        try 
        {
            TextMessage msg;
            int         i;

    
            System.err.println("Publishing to destination '"+name+"'\n");

            ConnectionFactory factory = new com.tibco.tibjms.TibjmsConnectionFactory(serverUrl);

            //No username
            connection = factory.createConnection("","");

            /* create the session */
            session = connection.createSession(false,javax.jms.Session.AUTO_ACKNOWLEDGE);

            /* create the destination */
            if(producerType == TrainMsgProducer.TOPIC)
                destination = session.createTopic(name);
            else
                destination = session.createQueue(name);

            /* create the producer */
            msgProducer = session.createProducer(null);


            /* create text message */
            msg = session.createTextMessage();

            /* set message text */
            msg.setText((String)message);

            /* publish message */
            msgProducer.send(destination, msg);
            System.out.println("Message sent: " + msg);

            /* close the connection */
            connection.close();
        } 
        catch (JMSException e) 
        {
            e.printStackTrace();
            System.exit(-1);
        }
    }
}