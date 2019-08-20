using System;
using System.Threading;
using Nixie_clock_esp32.Nixie;
using Nixie_clock_esp32.Clock;
using nanoFramework.Hardware.Esp32.RMT.Tx;

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

			var tx = Transmitter.Register(Config.LED_DATA_PIN);
			tx.ClockDivider = 4;

			var pulses = new PulseCommandList();
			pulses
				.AddState(false, PulseCommand.MAX_DURATION)
				.AddState(true, PulseCommand.MAX_DURATION)
				.AddState(false, PulseCommand.MAX_DURATION)
				.AddState(true, PulseCommand.MAX_DURATION)
				.AddState(false, 100)
				.AddState(false, 200)
				.AddState(true, 200)
				.AddState(false, 200);

			tx.Send(pulses);

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