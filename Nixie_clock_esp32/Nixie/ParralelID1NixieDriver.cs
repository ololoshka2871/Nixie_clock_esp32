using nanoFramework.Hardware.Esp32.DynamicIndication;

namespace Nixie_clock_esp32.Nixie
{
	class ParralelID1NixieDriver : INixieDriver
	{
		public ParralelID1NixieDriver(int[] dataPins, int[] selectorPins, int dataBitsPreIndicator, 
			IEncoder encoder)
		{
			controller = new Controller(dataPins, selectorPins, dataBitsPreIndicator);
			this.encoder = encoder;
		}

		public uint UpdatePeriod_us
		{
			get => controller.UpdatePeriod_us;
			set => controller.UpdatePeriod_us = value;
		}

		public bool Enabled
		{
			get => controller.Enabled;
			set => controller.Enabled = value;
		}

		public string Text
		{
			get => text;
			set
			{
				text = value;
				controller.SetData(encoder.Encode(text));
			}
		}

		private Controller controller;
		private IEncoder encoder;
		private string text;
	}
}
