﻿namespace Nixie_clock_esp32.Nixie
{
	/// <summary>
	/// Интерфейс для всего драйвера NIXIE индикаторов
	/// </summary>
	internal interface INixieDriver
	{
		bool Enabled { get; set; }
		string Text { get; set; }
	}
}