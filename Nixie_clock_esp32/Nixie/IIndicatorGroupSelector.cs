namespace Nixie_clock_esp32.Nixie
{
	/// <summary>
	/// Обычный сценарий использования
	/// selector.Enabled = false;
	/// var next_goup = selector.NextGroup();
	/// (set data for greoup)
	/// selector.Enabled = true;
	/// </summary>
	internal interface IIndicatorGroupSelector
	{
		/// <summary>
		/// Переключиться на следующую группу
		/// </summary>
		/// <returns>Номер группы, на которую переключились</returns>
		int NextGroup();

		/// <summary>
		/// Включить/выключить текущую группу
		/// </summary>
		bool Enabled { get; set; }

		/// <summary>
		/// Номер группы, активной в данный момет
		/// </summary>
		int CurrentGroup { get; }
	}
}