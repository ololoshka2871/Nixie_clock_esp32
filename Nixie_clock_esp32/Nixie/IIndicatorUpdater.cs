namespace Nixie_clock_esp32.Nixie
{
	/// <summary>
	/// Обновляет данные на индикаторе
	///
	/// Индикация может быть статической (регистры) или динамической, все инкапсулируется этим интерфейсом. Сами данные он берет из IRawDataBuffer
	/// </summary>
	internal interface IIndicatorUpdater
	{
		#region Methods

		/// <summary>
		/// Управление работой индикаторов
		///
		/// Для статической индикации должна выключать все сегменты
		/// Для динамической индикации останавливает обновление отключает выбор всех блоков
		/// </summary>
		bool Enabled { get; set; }

		#endregion Methods

		#region Properties

		/// <summary>
		/// сырые данные для индикаторов
		/// </summary>
		IRawDataBuffer Data { get; set; }

		#endregion Properties
	}
}