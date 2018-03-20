copy TrainListener.java C:\tibco\ems\6.0\samples\java
copy TrainMsgConsumer.java C:\tibco\ems\6.0\samples\java

cd C:\tibco\ems\6.0\samples\java
CALL C:\tibco\ems\6.0\samples\java\setup.bat

javac TrainListener.java
javac TrainMsgConsumer.java
java TrainListener -trainname Train_1

pause