from flask import Flask
from flask import Response
import requests
from xml.dom import minidom
import random
from xml.sax.saxutils import escape

app = Flask(__name__)


@app.route('/key/<path:key>')
def index(key):
    nea_api_url = "http://api.nea.gov.sg/api/WebAPI/?dataset=heavy_rain_warning&keyref=" + key
    r = requests.get(nea_api_url)
    if r.status_code == 200:
        xmlform = minidom.parseString(r.text)
        collection = xmlform.documentElement
        warning = collection.getElementsByTagName("warning")[0].childNodes[0].data
        warning = str(warning).rstrip().lstrip()


        if str(warning).rstrip() == 'NIL':
            random_no = random.uniform(0, 1)
            if random_no < 0.5:
                return Response("""<channel><title>Heavy Rain Warning</title><source></source>Meteorological Service Singapore <item><title>HEAVY RAIN WARNING</title><issue_datentime>-</issue_datentime>
                    <warning>""" + "TRUE" + """</warning></item><rain_area_image><metadata>null</metadata></rain_area_image><satellite_image><metadata>null</metadata></satellite_image></channel>""", mimetype='text/xml')

        return Response("""<channel><title>Heavy Rain Warning</title><source></source>Meteorological Service Singapore <item><title>HEAVY RAIN WARNING</title><issue_datentime>-</issue_datentime>
                    <warning>""" + escape(warning) + """</warning></item><rain_area_image><metadata>null</metadata></rain_area_image><satellite_image><metadata>null</metadata></satellite_image></channel>""", mimetype='text/xml')


@app.route('/')
def main():
    return "EI Weather API"

if __name__ == '__main__':
    app.run()
