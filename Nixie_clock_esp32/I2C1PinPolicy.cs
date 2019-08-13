using nanoFramework.Hardware.Esp32;

namespace Nixie_clock_esp32
{
	internal class I2C1PinPolicy : IPinFunctionPolicy
	{
		#region Fields

		private readonly int SDA_pin, SCL_pin;

		#endregion Fields

		#region Constructors

		public I2C1PinPolicy(int SDA_pin, int SCL_pin)
		{
			this.SCL_pin = SCL_pin;
			this.SDA_pin = SDA_pin;
		}

		#endregion Constructors

		#region Methods

		public void SetPinAlternate()
		{
			Configuration.SetPinFunction(SDA_pin, DeviceFunction.I2C1_DATA);
			Configuration.SetPinFunction(SCL_pin, DeviceFunction.I2C1_CLOCK);
		}

		#endregion Methods
	}
}