using System;

namespace BusDepot
{
	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Initialising BusDepotAsyncMsgConsumer...");
			BusDepotAsyncMsgConsumer jurongBusDepotMsgConsumer = new BusDepotAsyncMsgConsumer(new [] { "-server", "localhost", "-queue", "q.depot.jurong" });
			BusDepotAsyncMsgConsumer tampinesBusDepotMsgConsumer = new BusDepotAsyncMsgConsumer(new [] { "-server", "localhost", "-queue", "q.depot.tampines" });
			BusDepotAsyncMsgConsumer toapayohBusDepotMsgConsumer = new BusDepotAsyncMsgConsumer(new [] { "-server", "localhost", "-queue", "q.depot.toapayoh" });
			Console.WriteLine("Done with BusDepotAsyncMsgConsumer!");
			
			Console.ReadKey(true);
		}
	}
}