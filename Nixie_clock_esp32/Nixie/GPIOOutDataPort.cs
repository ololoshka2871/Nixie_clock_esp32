using Windows.Devices.Gpio;

namespace Nixie_clock_esp32.Nixie
{
	internal class GPIOOutDataPort : IOutDataPort
	{
		#region Fields

		private readonly GpioPin[] Pins;

		#endregion Fields

		#region Constructors

		/// <summary>
		/// Создать порт из уже зарегистрированных пинов
		///
		/// Номера пинов в массиве соответсвуют номерам биров в получаемом порте
		/// </summary>
		/// <param name="pins">Массив пинов для объединения в качестве порта</param>
		public GPIOOutDataPort(GpioPin[] pins)
		{
			Pins = pins;
		}

		/// <summary>
		/// Создать порт из пинов, переданных по номеру
		///
		/// Номера пинов в массиве соответсвуют номерам биров в получаемом порте
		/// </summary>
		/// <param name="pins">Массив номеров пинов для объединения в качестве порта</param>
		public GPIOOutDataPort(int[] pins, bool isOpendrain = false) : this(Pinn2Gpiopin(pins, isOpendrain)) { }

		#endregion Constructors

		#region Properties

		/// <summary>
		/// Инвертировать значения на порте
		/// </summary>
		public bool Inverted { get; set; } = false;

		/// <summary>
		/// Значение на порте
		/// </summary>
		public uint Value
		{
			get => TranslateToUint();
			set => SetPinsState(value);
		}

		public int Width => Pins.Length;

		#endregion Properties

		#region Methods

		private static GpioPin[] Pinn2Gpiopin(int[] pins, bool isOpendrain)
		{
			var controller = GpioController.GetDefault();
			var gpios = new GpioPin[pins.Length];
			for (int i = 0; i < pins.Length; ++i)
			{
				var pin = controller.OpenPin(pins[i]);
				pin.SetDriveMode(isOpendrain ? GpioPinDriveMode.OutputOpenDrain : GpioPinDriveMode.Output);
				gpios[i] = pin;
			}
			return gpios;
		}

		/// <summary>
		/// Установить пины согласно значению v
		/// </summary>
		private void SetPinsState(uint v)
		{
			for (int i = 0; i < Pins.Length; ++i)
			{
				Pins[i].Write(
					(((v & 1u << i) != 0) ^ Inverted) ? GpioPinValue.High : GpioPinValue.Low
					);
			}
		}

		/// <summary>
		/// Получить значение на порте в ввиде uint32
		/// </summary>
		private uint TranslateToUint()
		{
			uint res = 0;
			for (int i = 0; i < Pins.Length; ++i)
			{
				if ((Pins[i].Read() == GpioPinValue.High) ^ Inverted)
				{
					res |= 1u << i;
				}
			}
			return res;
		}

		#endregion Methods
	}
}