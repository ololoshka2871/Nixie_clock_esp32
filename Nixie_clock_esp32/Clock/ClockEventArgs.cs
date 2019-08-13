using System;

namespace Nixie_clock_esp32.Clock
{
	class ClockEventArgs
	{
		public readonly DateTime time;
		public readonly bool rtc_error;

		public ClockEventArgs(DateTime time, bool rtc_error)
		{
			this.time = time;
			this.rtc_error = rtc_error;
		}
	}
}
