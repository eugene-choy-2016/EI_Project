# Weather Rest API
This API wrapper will invoke a HTTP Request to the original NEA's heavy rain warning API at "http://api.nea.gov.sg/api/WebAPI/?dataset=heavy_rain_warning&keyref='REPLACE-NEA-KEY-HERE'"
In the event that there's no heavy rain warning, this wrapper will have a 50% chance of returning 'TRUE' aka 'heavy rain warning'.

### Prerequisites

* Python 2.7
* JetBrain PyCharm IDE
* NEA's API key ("https://www.nea.gov.sg/api/api/nea-s-datasets#Register")


## How to use it?
```
* Clone project
* Open project in PyCharm IDE
* Run 'Python.py'
```

## Sample output
```
<channel>
    <title>Heavy Rain Warning</title>
    <source></source>Meteorological Service Singapore
    <item>
        <title>HEAVY RAIN WARNING</title>
        <issue_datentime>-</issue_datentime>
        <warning>NIL</warning>
    </item>
    <rain_area_image>
        <metadata>null</metadata>
    </rain_area_image>
    <satellite_image>
        <metadata>null</metadata>
    </satellite_image>
</channel>
```
## Deployment
This Python wrapper has been deployed to Heroku Cloud.
Accessible via "https://ei-weather-api.herokuapp.com/key/'REPLACE-NEA-KEY-HERE'"