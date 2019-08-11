namespace Nixie_clock_esp32.Nixie
{
	/// <summary>
	/// Буфер для сырых данных, в формате, пригодном для непосредственной отправки на индикаторы
	/// </summary>
	internal interface IRawDataBuffer
	{
		#region Methods

		/// <summary>
		/// Скопировать данные из другого объекта, чтобы избежать фрагментации кучи.
		/// </summary>
		void From(IRawDataBuffer newstate);

		/// <summary>
		/// Получить сырую комбинацию по смещениею element_number
		/// Если запрашиваемый номер больше выделенной длины - возвращать 0
		/// </summary>
		uint Get(int element_number);

		/// <summary>
		/// Установить элемент element_number в значение value
		/// Если запрашиваемый номер больше длины - ни чего не делать
		/// </summary>
		IRawDataBuffer Set(int element_number, uint value);

		#endregion Methods
	}
}