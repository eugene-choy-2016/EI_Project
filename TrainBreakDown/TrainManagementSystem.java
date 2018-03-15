import java.util.*;
import javax.jms.*;
import java.io.IOException;

public class TrainManagementSystem{
    
    String serverUrl = null;
    
    
    public static void main(String[] args){
        Scanner sc = new Scanner(System.in);
        boolean run = true;
        severUrl = "localhost"; //if this is done locally 
        
        while(run){
            System.out.println();
            System.out.println();
            
            
            System.out.println("1. Update Server URL");
            System.out.println("2. Send Message");
            System.out.println("3. Check Incoming Message");
            System.out.println("4. Quit Program");
            System.out.println("test");
            
            int input = sc.nextInt();
            run = routeInput(input,sc);
            
            
            
        }

    }
    
    public boolean routeInput(int input, Scanner sc){
        `
        if(input == 1){
            updateURL(sc);
            return true;
        }
        if(input == 2){
            sendMessage(sc);
            return true;
        }
        if (input == 3){
            return true;
        }
        if(input == 4){
            
            return false;
        }
    }
    
    public void updateURL(Scanner sc){
        System.out.println(); 
        System.out.println("Current serverURL: "&& serverUrl);
        System.out.print("New serverURL: ");
        
        serverURL = sc.nextLine(); //Get the new serverURL and update it
        
    }
    
    public void sendMessage(Scanner sc){
        System.out.println();
        System.out.print("Indicate the destination you want to send to:");
        
        //Train breakdown
        // Weather Topic
        // Send message to trains 
        
    }
}
