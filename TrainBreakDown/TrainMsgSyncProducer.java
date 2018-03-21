import java.util.*;
import javax.jms.*;

public class TrainMsgSyncProducer {

    public static final int QUEUE = 1;
    public static final int TOPIC = 2;

    private String serverUrl;
    private String name;
    private String replyName;
    private int producerType;

    private Connection connection;
    private Session session;
    private MessageProducer msgProducer;
    private MessageConsumer msgConsumer;

    private Destination destination;
    private Destination replyDestination;

    public TrainMsgAsyncProducer(String serverUrl, String name, String replyName, int producerType) {
        this.serverUrl = serverUrl;
        this.name = name;
        this.replyName = replyName;
        this.producerType = producerType;
        try {
            tibjmsUtilities.initSSLParams(serverUrl, new String[0]);
        } catch (JMSSecurityException e) {
            System.err.println("JMSSecurityException: " + e.getMessage() + ", provider=" + e.getErrorCode());
            e.printStackTrace();
            System.exit(0);
        }
    }

    public void sendMessage(String message) {
        try {
            TextMessage msg;
            int i;

            System.err.println("Publishing to destination '" + name + "'\n");

            ConnectionFactory factory = new com.tibco.tibjms.TibjmsConnectionFactory(serverUrl);

            //No username
            connection = factory.createConnection("", "");

            /* create the session */
            session = connection.createSession(false, javax.jms.Session.AUTO_ACKNOWLEDGE);

            /* create the destination */
            if (producerType == TrainMsgProducer.TOPIC) {
                destination = session.createTopic(name);
                replyDestination = session.createTopic(replyName);
            } else {
                destination = session.createQueue(name);
                replyDestination = session.createQueue(replyName);
            }

            /* create the producer */
            msgProducer = session.createProducer(null);
            msgConsumer = session.createConsumer(replyDestination);
			connection.start();
            /* create text message */
            msg = session.createTextMessage();

            /* set message text */
            msg.setText((String) message);
            msg.setJMSReplyTo(replyDestination);

            /* publish message */
            msgProducer.send(destination, msg);
            System.out.println("Message deployed awaiting reply...");
            //Wait for a reply regarding Bus Deployed
            // Send a request and wait for a reply. Code also can be added to time-out the wait
            Message reply = msgConsumer.receive();

            // Process the reply.
            printMsg(reply);

            /* close the connection */
            connection.close();
        } catch (JMSException e) {
            e.printStackTrace();
            System.exit(-1);
        }
    }

    public void printMsg(Message msg) throws JMSException {
        if (msg instanceof TextMessage) {
            TextMessage replyMessage = (TextMessage) msg;
            System.out.println("Received reply ");
            System.out.println("\tTime:       " + System.currentTimeMillis() + " ms");
            System.out.println("\tMessage ID: " + replyMessage.getJMSMessageID());
            System.out.println("\tCorrel. ID: " + replyMessage.getJMSCorrelationID());
            System.out.println("\tReply to:   " + replyMessage.getJMSReplyTo());
            System.out.println("\tContents:   " + replyMessage.getText());
        } else {
            System.out.println("Invalid message detected");
            System.out.println("\tType:       " + msg.getClass().getName());
            System.out.println("\tTime:       " + System.currentTimeMillis() + " ms");
            System.out.println("\tMessage ID: " + msg.getJMSMessageID());
            System.out.println("\tCorrel. ID: " + msg.getJMSCorrelationID());
            System.out.println("\tReply to:   " + msg.getJMSReplyTo());

            msg.setJMSCorrelationID(msg.getJMSMessageID());

        }
    }

}