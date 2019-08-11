namespace Nixie_clock_esp32.Nixie
{
	interface IDataPolicy
	{
		/// <summary>
		/// Выставить на шину данных нужный фрагмент из ВСЕХ данных согласно группе.
		/// </summary>
		void WriteGroup(IRawDataBuffer rawdata, int group_number);
	}
}
