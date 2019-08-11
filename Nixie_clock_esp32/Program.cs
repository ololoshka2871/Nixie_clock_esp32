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
			/*
			var nixie_ctrl = new DynamicIndicatorUpdater(
				new ID1Datapolicy(new GPIOOutDataPort(Config.NixieDataPortPins)),
				new BasicGroupSelector(new GPIOOutDataPort(Config.NixieCtrlPort)))
			{
				Data = new ByteDataBuffer(6),
				Enabled = true
			};

			var ndp0 = new GPIODataPort(Config.NixieDataPortPins);
			var gctrl = new GPIODataPort(Config.NixieCtrlPort);

			ndp0.Value = 0x53;

			while (true)
			{
				gctrl.Value = 0b001;
				Thread.Sleep(20);
				gctrl.Value = 0b010;
				Thread.Sleep(20);
				gctrl.Value = 0b100;
				Thread.Sleep(20);
			}
			*/


			Controller updater = new Controller(Config.NixieDataPortPins, Config.NixieCtrlPort, 4);
			var testdata = new uint[] { 2, 3, 4, 5, 6, 7 };
			updater.SetData(testdata);
			
			if (updater.TestData(testdata))
			{
				Console.WriteLine("Test passed!");
			} else
			{
				Console.WriteLine("Test failed!");
			}
			updater.Enabled = true;

			Thread.Sleep(Timeout.Infinite);
		}
	}
}