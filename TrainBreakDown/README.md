# Train Management System

## How to use it?
Run the compileTMS.bat file, it will copy the relevant Java files into tibco/ems/sample/java and compile.
It will then run the file. 

## Parameters
Please use this names for the various JMS Messages

```
Breakdown messages : q.breakdown
```

## Features

### Send Message Feature
This function will read all the xml files in EI_Project/TrainBreakDown/msgResource and send it as a JMS message.
No other configurations are needed

### Receive Message Feature
Yet to be implemented