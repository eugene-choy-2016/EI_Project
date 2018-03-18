This process will actively poll a folder for new schedule (in .txt format) and upload to storage for dispersal to other Depots. Once the file poller detects a new file, it will invoke the upload schedule API to upload the schedule and disperse it to the depots via email.

Tibco Configuration:
The schedule poller's File Name in the Configuration tab will point to the folder it is observing. Change if need to. The *.txt catches all .txt file.

Send Email activity requires manual confiugration of the SMTP server with authentication and changing settings in Tibco files.
https://stackoverflow.com/questions/23893424/how-to-send-email-using-tibco-mail-activity
Go to "<tibco installation folder>/designer/5.8/bin/designer.tra" and add the following lines:
java.property.mail.smtp.starttls.enable=true
java.property.mail.smtp.starttls.required=true
Save the file and restart Tibco Designer.

Download the entire certificate chain of your email provider's SMTP and put in the certs folder. Then in email activity "Configure SSL", select the certs folder. Tick the authentication box and type in your username and password of your SMTP login (most probably your normal login ones).

API:
It is to note that the API strictly only accepts POST