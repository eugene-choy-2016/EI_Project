@echo off

set CSLIB=.\TIBCO.EMS.dll

C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /r:%CSLIB% BusDepotAsyncMsgConsumer.cs Program.cs