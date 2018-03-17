This process will actively poll a folder for new schedule (in .txt format) and upload to storage for dispersal to other Depots. Once the file poller detects a new file, it will invoke the upload schedule API to upload the schedule and disperse it to the depots via email.

Tibco Configuration:
The schedule poller's File Name in the Configuration tab will point to the folder it is observing. Change if need to. The *.txt catches all .txt file.

API:
It is to note that the API strictly only accepts POST