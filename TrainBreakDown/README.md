# Train Management System & Train Listeners
Train Management System is the "command center" for the Train Network system. When the system is down or system has resumed, a message
will be deployed from here.

Train Listeners represent individual Train that is tuning to various topics to receive messages that may affect how the train should travel.

## How to use it?
Train Management System: Run the compileTMS.bat file. The bat file will copy the relevant Java and XSD files into tibco/ems/sample/java as well as
copy the XML into "C:\EI\Project\breakdownReports" and compile.
It will then run the file.

Train Listeners 1 & 2 : Run the respective runTrainListener1.bat and runTrainListener2.bat, the Java file for TrainListener will be copied to tibco/ems/sample/java
and compiled. The instances will then be run with their respective Train Names

## Parameters
Please use this names for the various JMS Messages

```
Breakdown messages : q.breakdown
```

```
Async Reply after deployed: q.deployed
```

```
Resume of service : q.resumed
```

```
East-West Line: t.ew.listeners
North-South Line: t.ns.listeners
```

```
Weather related messages : t.weather
```

## Train Management System
This is the "command center" for all the trains. Any breakdown and resume of service messages will be deployed from here

### 1. Update Server URL
This allows the TMS to be configured into the other parties URL for easy sending of messages. The MsgProducer and
AsyncMsgProducer will be reinitialized with the new server URl after updated.


### 2. Send Breakdown Msg
Upon execution of sending breakdown msg,TMS will read all the files in the directory for XML and send it as a JMS message to q.breakdown. It will then wait for a reply by
TIBCO to indicate that buses haven been deployed accordingly.


### 3. Send System Resumed Messages
A standard "SYSTEM RESUMED" message will be sent to q.resumed to indicate that all services have resumed.

## Train Listeners
The train Listener will first prompt the user to log in to either to East-West or North-South line. Once the choice has been
made the train listener will be listening to 2 topics, mainly the t.weather and the respective lines listener t.<dynamic>.listeners

