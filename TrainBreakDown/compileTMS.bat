copy TrainManagementSystem.java C:\tibco\ems\6.0\samples\java
copy TrainMsgProducer.java C:\tibco\ems\6.0\samples\java

if not exist "C:\EI\Project\breakdownReports\" mkdir C:\EI\Project\breakdownReports\

copy .\msgResource\ C:\EI\Project\breakdownReports\
copy new_breakdown.xsd C:\tibco\ems\6.0\samples\java

cd C:\tibco\ems\6.0\samples\java
CALL C:\tibco\ems\6.0\samples\java\setup.bat

javac TrainManagementSystem.java
javac TrainMsgProducer.java


java TrainManagementSystem

pause