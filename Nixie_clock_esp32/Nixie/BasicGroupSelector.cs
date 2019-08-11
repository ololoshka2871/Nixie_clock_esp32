using System;

namespace Nixie_clock_esp32.Nixie
{
	internal class BasicGroupSelector : IIndicatorGroupSelector
	{
		#region Fields

		/// <summary>
		/// Количество групп
		/// </summary>
		private readonly int GroupCount;

		/// <summary>
		/// Порт данных для выбора (1 бит из N)
		/// </summary>
		private readonly IOutDataPort Port;

		private bool enabled = false;

		#endregion Fields

		#region Constructors

		public BasicGroupSelector(IOutDataPort port)
		{
			Port = port;
			var group_count = port.Width;
			if (group_count > 32)
			{
				throw new ArgumentOutOfRangeException("Maximum number of groups is 32");
			}
			GroupCount = group_count;
		}

		#endregion Constructors

		#region Properties

		/// <summary>
		/// Текущая выбранная группа
		/// </summary>
		public int CurrentGroup { get; private set; }

		public bool Enabled
		{
			get => enabled;
			set
			{
				enabled = value;
				if (enabled)
				{
					Port.Value = 1u << CurrentGroup;
				}
				else
				{
					Port.Value = 0;
				}
			}
		}

		#endregion Properties

		#region Methods

		public int NextGroup()
		{
			var was_enabled = Enabled;
			CurrentGroup = (CurrentGroup + 1) % GroupCount;
			if (was_enabled)
			{
				Enabled = true;
			}

			return CurrentGroup;
		}

		#endregion Methods
	}
}