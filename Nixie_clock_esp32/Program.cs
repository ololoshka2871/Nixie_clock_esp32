using System;
using System.Threading;
using Nixie_clock_esp32.Nixie;
using Nixie_clock_esp32.Clock;
using nanoFramework.Hardware.Esp32.RMT.NeoPixel;

namespace Nixie_clock_esp32
{
	public class Program
	{
		private static ParralelID1NixieDriver NixieCtrl;

		private static void UpdateDate(object sender, ClockEventArgs arg)
		{
			NixieCtrl.Text = DateTimePrinter.PrintTime(arg.time);
		}

		public static void Main()
		{
			Console.WriteLine("Hello world!");

			var strip = new NeopixelChain(2, 6);
			strip[0] = new Color() { R = 2, G = 1, B = 3 };
			strip[1] = new Color() { R = 50, G = 50, B = 50 };
			strip[2] = new Color() { R = 100, G = 100, B = 100 };
			strip[3] = new Color() { R = 150, G = 150, B = 150 };
			strip[4] = new Color() { R = 200, G = 200, B = 200 };
			strip[5] = new Color() { R = 0xff, G = 0xff, B = 0xff };
			strip.Update();

			/*
			var rtc_controller = new RTC_Controller("I2C1", Config.SQW, 
				new I2C1PinPolicy(Config.SDA, Config.SCL));
			rtc_controller.On1Spassed += UpdateDate;

			NixieCtrl = new ParralelID1NixieDriver(Config.NixieDataPortPins, 
				Config.NixieCtrlPort, ID1DataEncoder.DataBitsPreIndicator, new ID1DataEncoder(Config.EncodeSymbol))
			{
				UpdatePeriod_us = 5000,
				Enabled = true
			};
			*/
			Thread.Sleep(Timeout.Infinite);
		}
	}
}