using System;
using System.Globalization;
using ModbusTCPdelta_Library;

namespace ModbusTCPdelta_UsingSample
{
	public class Program
	{
		//some boolean field
		public static string MyFieldBoolean { get; set; }

		//connection to controller state
		public static bool IsConnected { get; set; }

		//subscription to some register state to update field value
		public static void SubscriptionMyField(string startAddress)
		{
			if (IsConnected)
			{
				MyFieldBoolean = Hard.ReadBoolean(startAddress);
			}
		}

		//subscription update period, in milliseconds
		private static int subscriptionUpdateTime = 500;

		//connection state check period, in milliseconds
		private static int connectionUpdateTime = 100;

		private static ModbusTCP Hard;

		static void Main(string[] args)
		{
			Hard = new ModbusTCP("192.168.1.1", 502, connectionUpdateTime);
			Hard.SetConnectionStatus += ConnectionStatus;

			string addrM123 = "M123";
			string addrD194 = "D194";

			//read boolean register state
			var a = Hard.ReadBoolean(addrM123);
			Console.WriteLine(a);

			//if we got not empty value, change register state
			if (a.Length > 0)
			{
				Hard.WriteBoolean(addrM123, !Convert.ToBoolean(a));
			}

			//read decimal register value 
			var b = Hard.ReadDecimal(addrD194);
			Console.WriteLine(b);

			//write another value
			Hard.WriteDecimal(addrD194, 50);

			//subscrube to some boolean register state
			Hard.Subscribe(addrM123, SubscriptionMyField, subscriptionUpdateTime);

			Console.ReadKey();
		}

		private static void ConnectionStatus(bool status)
		{
			IsConnected = status;
		}
	}
}
