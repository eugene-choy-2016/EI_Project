# Train Management System

## How to use it?
Run the compileTMS.bat file. The bat file will copy the relevant Java and XSD files into tibco/ems/sample/java as well as
copy the XML into "C:\EI\Project\breakdownReports" and compile.
It will then run the file. 

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

## Features

### 1. Update Server URL
This allows the TMS to be configured into the other parties URL for easy sending of messages. The MsgProducer and
AsyncMsgProducer will be reinitialized with the new server URl after updated.


### 2. Send Breakdown Msg
Upon execution of sending breakdown msg,TMS will read all the files in the directory for XML and send it as a JMS message to q.breakdown. It will then wait for a reply by
TIBCO to indicate that buses haven been deployed accordingly.


### 3. Send System Resumed Messages
A standard "SYSTEM RESUMED" message will be sent to q.resumed to indicate that all services have resumed.

