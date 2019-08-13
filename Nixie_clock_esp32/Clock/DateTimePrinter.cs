using System;

namespace Nixie_clock_esp32.Clock
{
	internal static class DateTimePrinter
	{
		public static string PrintTime(DateTime time) => time.ToString("HHmmss");
		public static string PrintDate(DateTime time) => time.ToString("ddMMy");
	}
}
