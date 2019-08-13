namespace Nixie_clock_esp32
{
	internal class Config
	{
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

		/// <summary>
		/// Таблица пинов, включающих лампы
		/// </summary>
		public static readonly int[] NixieCtrlPort = new int[]
		{
			13, // TG0
			2,	// TG1
			14	// TG2
		};

		public const int SDA = 21;
		public const int SCL = 22;
		public const int SQW = 23;
	}
}