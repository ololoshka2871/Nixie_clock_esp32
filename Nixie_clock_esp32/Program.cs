using System;
using System.Threading;
using nanoFramework.Hardware.Esp32.DynamicIndication;

namespace Nixie_clock_esp32
{
	public class Program
	{
		public static void Main()
		{
			Console.WriteLine("Hello world!");

			var rtc_controller = new RTC_Controller("I2C1", 
				Config.SQW, new I2C1PinPolicy(Config.SDA, Config.SCL));

			Controller updater = new Controller(Config.NixieDataPortPins, Config.NixieCtrlPort, 4);
			updater.UpdatePeriod_us = 5000;
			updater.Enabled = true;

			for (int i = 0; i < 2; ++i)
			{
				// 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | off > На лампе
				// 3 | 2 | 4 | 1 | 8 | 13| 5 | 12| 9 | 0 | 10  > На входе
				updater.SetData(new uint[] { 3, 2, 4, 1, 8, 13 });
				Thread.Sleep(3000);
				updater.SetData(new uint[] { 5, 12, 9, 0, 10, 10 });
				Thread.Sleep(3000);
			}
					   
			Thread.Sleep(Timeout.Infinite);
		}
	}
}