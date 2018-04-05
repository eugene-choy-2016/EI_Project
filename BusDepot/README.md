# BusDepot Consumers
BusDepot Consumers are programs that are represented by the respective queues that they wait on.

For simplicity's sake, running the program will emulate all 3 bus depots in our scenario in a single screen.

## How to use it?
In the BusDepot/BusDepot folder, open a command prompt in that folder and run the `build.bat` file. A `Program.exe` will appear in the current folder.

Next, simply run the `Program.exe` and it will start listening to `q.depot.<dynamic>`. Ensure that the TIBCO EMS process is running beforehand.