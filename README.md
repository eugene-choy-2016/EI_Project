# EI_Project
EI Project for Group 3 G2 EI class. The group members for the group are:
1. Eugene Choy Wen Jia
2. Ho Min Kit Winston
3. Ho Wei Hong
4. Sim Li Jin
5. Yin Yukun
6. Yong Fu Xiang

## Introduction
The project simulates the integration processes of the Public Transport System in several scenarios. This scenarios include:
1. Train Breakdown and Resumption of Service
2. Weather Reporting
3. Schedule Polling
4. Nearest Bus Depot Finder 

## File Structure
The file structure in this directory is as follow:

| Folder | Description | 
| --- | --- |
| [Bus Depot](/BusDepot) | Used for Train Breakdown process |
| [Documentations](/Documentations) | Process Images, Powerpoints and Tech document for Project |
| [Main Project File](/Tibco%20Project%20Files/Tibco_Project) | The Main Project file containing all the processess for Tibco |
| [Train Breakdown](/TrainBreakDown) |  Used for Train Breakdown Management process |
| [Schedule Polling](/schedulepolling) |  Used for Schedule Polling process |
| [Weather Reporting](/WeatherReporting) |  Files used for Weather Reporting process |
| [Web Service](/Web%20Service) | Locally-hosted coded web service for nearest bus depot (Train Breakdown process) |

There are various **Readme.md** in the various folders to help user better understand how the files are supposed to be interacted by the user

## Train Breakdown and Resumption of Service
This process simulates integration across Bus Depot, Train Management System, Train Listeners, Email and Twitter using the Train Breakdown and Resumption of Service Process created with Tibco BW.

Detailed instructions of how to run the Bus Depot, Train Management System and Train Listeners can be found in their respective folder.

The user is also required to open the Netbeans project found in Web Service folder and run it (This webservice is hosted on Localhost).

For Twitter to work, some configurations may have to be made by User to Tibco BW.

## Weather Reporting
This process invokes a REST call to the NEA API every few seconds (pre-set by user) to poll for the weather to check if there is heavy rain fall. For demo purpose, the team has wrote a wrapper with Python to give it a 50% chance of rainfall if NEA API returns a nil for rainfall. This wrapper is hosted on Heroku

## Schedule Polling
This process will be triggered once there is a change to the folder C:/EI/Project/schedules and when there is an addition of a txt file (which simulates the schedule for Bus Depots). The file will be uploaded to a webservice and a URL will be generated and email to all the bus depots for them to download the schedule. More detailed instructions can be found in the schedule polling's folder README

