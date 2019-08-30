using System;
using System.Threading;
using Nixie_clock_esp32.Nixie;
using Nixie_clock_esp32.Clock;
using nanoFramework.Hardware.Esp32;
using nanoFramework.Hardware.Esp32.RMT.NeoPixel;

namespace Nixie_clock_esp32
{
	public class Program
	{
		private static ParralelID1NixieDriver NixieCtrl;
		private static NeopixelChain strip;
		private static HighResTimer timer;

		private const int round = 1000;
		private static int c = 0;
		private const int LedPeriod_ms = 20;
		private static float Hstep;

		const int MaxH = 360;
		const float H1ms = MaxH / (float)round;

		private static void UpdateDate(object sender, ClockEventArgs arg)
		{
			NixieCtrl.Text = DateTimePrinter.PrintTime(arg.time);
			if (c >= round)
			{
				c = 0;
			}
		}

		private static void update_color(HighResTimer sender, object e)
		{
			if (c <= round)
			{
				var srcH = c * H1ms;

				for (int i = 0; i < strip.Size; ++i)
				{
					strip[i].SetHSV((int)((srcH + Hstep * i) % MaxH), 1.0f, 1.0f);
				}
				strip.Update();

				c += LedPeriod_ms;
			}
		}

		public static void Main()
		{
			Console.WriteLine("Hello world!");

			strip = new NeopixelChain(Config.LED_DATA_PIN, Config.LEDS_COUNT);

			timer = new HighResTimer();
			timer.OnHighResTimerExpired += update_color;
			Hstep = MaxH / (float)strip.Size;

			timer.StartOnePeriodic(LedPeriod_ms * 1000);
			/*
			strip.Update();
			*/

			/*
			var tx = nanoFramework.Hardware.Esp32.RMT.Tx.Transmitter.Register(Config.LED_DATA_PIN);
			tx.CarierEnabled = false;
			tx.TransmitIdleLevel = true;
			tx.IsTransmitIdleEnabled = true;
			tx.ClockDivider = 4;

			var cmd = new nanoFramework.Hardware.Esp32.RMT.Tx.PulseCommandList();
			cmd.AddLevel(true, 0x100).AddLevel(false, 0x50);
			while(true)
				tx.Send(cmd);
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
			
			/*
			while (true)
				update_color(null, null);
				*/

			Thread.Sleep(Timeout.Infinite);
		}
	}
}