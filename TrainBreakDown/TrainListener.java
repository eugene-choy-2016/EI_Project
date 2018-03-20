import java.util.*;
import javax.jms.*;
import java.io.*;
import java.lang.*;
import com.tibco.tibems.ufo.*;

public class TrainListener{
    
    private static String trainName;
    /*-----------------------------------------------------------------------
     * parseArgs
     *----------------------------------------------------------------------*/
    private static void parseArgs(String[] args)
    {
        int i=0;

        while(i < args.length)
        {
            if (args[i].compareTo("-trainname")==0)
            {
                
                trainName = args[i+1];
                i += 2;
                
            }   
        }
    }
    
    public static void main(String[] args){
        
        parseArgs(args);
        Scanner sc = new Scanner(System.in);
        
        TrainMsgConsumer weatherTopic = new TrainMsgConsumer("localhost","t.weather",TrainMsgConsumer.TOPIC);
        TrainMsgConsumer lineMessages;
        
        System.out.println("====================================");
        System.out.println("       Train Name: " +trainName+"    ");
        System.out.println("====================================");
        
        int line = 99;
        
        while (line < 1 || line > 2){
            System.out.println("Please log in to the correct Line: ");
            System.out.println("1. East-West (ew)");
            System.out.println("1. North-South (ns)");
            line = sc.nextInt();
        }
        
        if(line == 1){
            lineMessages = new TrainMsgConsumer("localhost","t.ew.listener",TrainMsgConsumer.TOPIC);
        }else{
            lineMessages = new TrainMsgConsumer("localhost","t.ew.listener",TrainMsgConsumer.TOPIC);
        }
        
        System.out.println();
        System.out.println();

    }
    
    
  
}
