using System;

namespace BusDepot
{
	class Program
	{
		public static void Main(string[] args)
		{
			string server = "localhost";
			if (args.Length >= 2)
				for (int i = 0; i < args.Length - 1; i++)
					if (args[i].Equals("-server"))
						server = args[i + 1];
		
			Console.WriteLine("Initialising BusDepotAsyncMsgConsumer...");
			BusDepotAsyncMsgConsumer jurongBusDepotMsgConsumer = new BusDepotAsyncMsgConsumer(new [] { "-server", server, "-queue", "q.depot.jurong" });
			BusDepotAsyncMsgConsumer tampinesBusDepotMsgConsumer = new BusDepotAsyncMsgConsumer(new [] { "-server", server, "-queue", "q.depot.tampines" });
			BusDepotAsyncMsgConsumer toapayohBusDepotMsgConsumer = new BusDepotAsyncMsgConsumer(new [] { "-server", server, "-queue", "q.depot.toapayoh" });
			Console.WriteLine("Done with BusDepotAsyncMsgConsumer!");
			
			Console.ReadKey(true);
		}
	}
}