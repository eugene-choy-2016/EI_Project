## NearestDepot
This Web service is invoked with the coordinates of the train station affected.
this web service is a REST webservice, it is invoked with an string representation of a xml file, the service will test the integrity of the xml according to a xsd schema.
The web service will return a JSON response containing the depot id of the nearest depot to the affected train station if the test passes, otherwise, it will return a error JSON message.

## Startup
This webservice should be started up before other process in Tibco BW is launched and initialized.

The web service will run on the local host.  In order to do so, the netbeans project must be started, and the webservice must be run once to initialize the local tomcat server. After which, the webservice will be ready to receive requests from other tibco activities.

