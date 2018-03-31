# Schedule Poller
![Schedule Polling Process](../Documentations/processes%20Image/Schedule%20Polling.PNG)

This process will scan a determined folder, when there is a new Schedule (in .txt) is added to the folder it will upload the schedule to a Web Storage by calling an API. The API will return a JSON containing the link to the schedule which will be sent to all depots' email.

## API
The API *(uploadschedule.php)* is written in PHP and hosted on **Azure App Service** with a **Azure Blob Storage** as the storage account. The access to Azure Blob Storage is configured based on the *blobstorageconfig.ini* which will be parsed in the PHP file for authentication and access to the storage. The container of the Blob Storage is also described in the ini file.

### Server Config
App Service needs to be able to serve PHP web pages and is tested on PHP *v7.2*; any other versions are not tested. **Composer** extension needs to be installed on the server for the API to work.

### Notes
API will only work with POST and will not accept any other types of HTTP requests.

## Process
There are a total of 4 activities in the process.

### Schedule Poller
This activity checks the designated folder for new schedule every 5 seconds. To change the designated folder, in the **Configuration** tab of the activity, choose the folder under **File Name:** and pressing the binoculars icon. The selector will force you to select a file, select one in the designated folder and edit the URL to remove the reference to the selected file. Alternatively, you can use a __*__.extension to only check for specific file format files.

### Group
Upload Schedule, Sleep and Write to Log activities are grouped together in a loop. When the process gets to Upload Schedule, it will attempt to upload the schedule. If it fails to do so, it will attempt to retry for 10 tries at an interval of 10 seconds. Each time it will log the failure in the Log file timestamped. The log file is found (and created) at

` C:\EI\Project\log.txt `

If at any point within the 10 tries, it manage to successful upload, the process will continue. Otherwise, it will end the process instance instantly after the 10th tries. 

### Upload Schedule
This activity calls the API to upload the schedule via POST. The URL of the API is configured in the **Configuration** tab in **Resource URL**. The key for the upload file is **schedule** and should (already) be set in **Input** tab's *Activity Input*, expanding the dropdown *Activity Input > Parameter > Body > Multipart > name* typing into the field.

It will return a JSON upon successful upload with *status, statusCode and link* as the JSON fields.

### Parse JSON
This activity will parse the JSON returned and extract the link for the next activity usage

### Write to Log-2
This activity is called if the result of the JSON returns `"status": "error"` where it will be logged into the log file as mentioned above.

### Send Emails to Depot
Otherwise, this activity will take the link extracted by the previous activity and send this link to other depots' email automatically.

#### Prerequisites
Due to the archaic nature of the palette, modifications need to be done for this activity to work.
<https://stackoverflow.com/a/23909223>

1. Add the following 2 lines to designer.tra (*\<Tibco installation folder\>/designer/5.8/bin*) at the bottom of the file:

`java.property.mail.smtp.starttls.enable=true` <br/>   `java.property.mail.smtp.starttls.required=true`

2. Restart Tibco Designer
3. If you are using other email provider aside from Gmail, you will be required to download the entire SMTP server's certificate chain of your email provider including the root CA. Placed them in the *certs* folder at the root of this Project folder.
4. If you are using Gmail as the sender, you will need to *allow* less secure apps from here: [Enable less secure app access](https://myaccount.google.com/lesssecureapps)

#### Configuration
In the **Configuration** tab:
- Host: Enter the SMTP server URL of your email provider including the port (mail.me.com:port)
- Use SSL?: Checked for today's email providers. Click *Configure SSL* if you are specifying certs in other folder. Certificate is REQUIRED.
- Authenticate?: Checked to enter your login details for your email

In the **Input** tab's *Activity Input*, expand the dropdown. All fields should be **double quoted** (""). Fill in the *username* and *password* of your sender. *from* field should be the email address of your sender. *to* field should be your receipient email. If you are sending to multiple, use a comma to seperate them in the same double quotes. Content of the message is setup in *bodyElement > bodyText*.

#### Project's Use
For this project these emails will be in use.
- **Sender**: depot.schedule@gmail.com
- **Receiver**: depot.jurong@gmail.com, depot.tampines@gmail.com

