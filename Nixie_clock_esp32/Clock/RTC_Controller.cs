using nanoFramework.Runtime.Native;
using nF.Devices.DS1307;
using System;
using Windows.Devices.Gpio;

namespace Nixie_clock_esp32.Clock
{
	internal class RTC_Controller : IDisposable
	{
		#region Delegates

		public delegate void SecondsCb(object sender, ClockEventArgs arg);

		#endregion Delegates

		#region Events

		public event SecondsCb On1Spassed;

		#endregion Events

		#region Constructors

		public RTC_Controller(string I2Cbus, int SQW_pin_n,
			IPinFunctionPolicy i2c_pin_policy)
		{
			i2c_pin_policy.SetPinAlternate();
			clock = DS1307.CreateDevice(I2Cbus);
			SQW_pin = GpioController.GetDefault().OpenPin(SQW_pin_n);
			SQW_pin.SetDriveMode(GpioPinDriveMode.Input);

			Sync_clocks();
			Enable_SQW();
		}

		#endregion Constructors

		#region Destructors

		~RTC_Controller()
		{
			Dispose(false);
		}

		#endregion Destructors

		#region Methods

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			SQW_pin.Dispose();
		}

		private void Enable_SQW()
		{
			clock.SetSquareWave(DS1307.SquareWaveFrequency.SQW_1Hz,
				DS1307.SquareWaveDisabledOutputControl.One);

			SQW_pin.ValueChanged += Sync_clocks;
		}

		private void Sync_clocks(object sender, GpioPinValueChangedEventArgs e)
					=> Sync_clocks();

		private void Sync_clocks()
		{
			var time = clock.GetDateTime();
			if (time.Year < 2100)
			{
				Rtc.SetSystemTime(time);
				InvokeOn1Spassed(time);
			} else
			{
				InvokeOn1Spassed(DateTime.UtcNow, false);
			}
		}

		private void InvokeOn1Spassed(DateTime time, bool rtc_error = false)
		{
			On1Spassed?.Invoke(this, new ClockEventArgs(time, rtc_error));
		}

		#endregion Methods

		#region Fields

		private readonly DS1307 clock;
		private GpioPin SQW_pin;

		#endregion Fields
	}
}