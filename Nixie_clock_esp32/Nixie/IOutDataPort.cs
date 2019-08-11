namespace Nixie_clock_esp32.Nixie
{
	/// <summary>
	/// Абстрактная группа выходов
	/// </summary>
	internal interface IOutDataPort
	{
		#region Properties

		/// <summary>
		/// Инвертировать значения на порте
		/// </summary>
		bool Inverted { get; set; }

		/// <summary>
		/// Установить/получить значение порта, не все биты могут фактически смотреть "на улицу", это только абстракция для группировки
		/// </summary>
		uint Value { get; set; }

		/// <summary>
		/// Ширина порта
		/// </summary>
		int Width { get; }

		#endregion Properties
	}
}