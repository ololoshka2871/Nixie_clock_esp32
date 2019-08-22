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
		private static NeopixelChain strip;

		private const int round = 5000;
		private static int c = 0;

		private static void UpdateDate(object sender, ClockEventArgs arg)
		{
			NixieCtrl.Text = DateTimePrinter.PrintTime(arg.time);
		}

		private static void update_color(object state)
		{
			const int MaxH = 360 * 512;
			const int H1ms = MaxH / round;
			
			int Hstep = 500 * H1ms / (int)strip.Size;
			var srcH = c * H1ms;

			for (uint i = 0; i < strip.Size; ++i)
			{
				strip[i].SetHSV((int)((srcH + Hstep * i) % MaxH), 255, 255);
			}
			strip.Update();

			c = (c + 10) % round;
		}

		public static void Main()
		{
			Console.WriteLine("Hello world!");

			strip = new NeopixelChain(Config.LED_DATA_PIN, Config.LEDS_COUNT);
			
			var timer = new Timer(update_color, null, 0, 10);
			/*
			strip[0] = new Color() { R = 255, G = 0, B = 0 };
			strip[1] = new Color() { R = 0, G = 255, B = 0 };
			strip[2] = new Color() { R = 0, G = 0, B = 255 };
			strip[3] = new Color() { R = 127, G = 0, B = 127 };
			strip[4] = new Color() { R = 127, G = 127, B = 0 };
			strip[5] = new Color() { R = 0, G = 127, B = 127 };
			strip.Update();
			*/

			var rtc_controller = new RTC_Controller("I2C1", Config.SQW, 
				new I2C1PinPolicy(Config.SDA, Config.SCL));
			rtc_controller.On1Spassed += UpdateDate;

			NixieCtrl = new ParralelID1NixieDriver(Config.NixieDataPortPins, 
				Config.NixieCtrlPort, ID1DataEncoder.DataBitsPreIndicator, new ID1DataEncoder(Config.EncodeSymbol))
			{
				UpdatePeriod_us = 5000,
				Enabled = true
			};

			Thread.Sleep(Timeout.Infinite);
		}
	}
}