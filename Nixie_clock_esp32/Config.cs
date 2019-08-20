using System;

namespace Nixie_clock_esp32
{
	internal class Config
	{
		#region Fields

		public const int SCL = 22;

		public const int SDA = 21;

		public const int SQW = 23;

		/// <summary>
		/// Таблица пинов, включающих лампы
		/// </summary>
		public static readonly int[] NixieCtrlPort = new int[]
		{
			13, // TG0
			2,	// TG1
			14	// TG2
		};

		/// <summary>
		/// Таблица пинов подключения двух 155ИД1
		/// </summary>
		public static readonly int[] NixieDataPortPins = new int[]
		{
			27, // D1A
			26, // D2A
			25, // D3A
			33, // D4A
			32, // D1B
			5,	// D2B
			18,	// D3B
			19	// D4B
		};

		public static readonly int LED_DATA_PIN = 15;

		#endregion Fields

		#region Methods

		public static uint EncodeSymbol(char simbol)
		{
			// 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | off > На лампе
			// 3 | 2 | 4 | 1 | 8 | 13| 5 | 12| 9 | 0 | 10  > На входе
			switch (simbol)
			{
				case '0': return 3;
				case '1': return 2;
				case '2': return 4;
				case '3': return 1;
				case '4': return 8;
				case '5': return 13;
				case '6': return 5;
				case '7': return 12;
				case '8': return 9;
				case '9': return 0;
				default: return 10;
			}
		}

		#endregion Methods
	}
}