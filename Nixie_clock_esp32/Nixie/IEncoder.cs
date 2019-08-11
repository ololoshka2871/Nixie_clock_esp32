namespace Nixie_clock_esp32.Nixie
{
	/// <summary>
	/// Конвертор строки в сырые данные для индикатора
	/// </summary>
	internal interface IEncoder
	{
		#region Methods

		/// <summary>
		/// Преобразует строку символов в массив кодов, пригодных для непосредственной отправки на индикаторы
		/// </summary>
		/// <param name="input">Строка символов</param>
		/// <returns>Абстрактный объект, содержащий набор кодов для отправки на индикаторы</returns>
		IRawDataBuffer Encode(string input);

		#endregion Methods
	}
}