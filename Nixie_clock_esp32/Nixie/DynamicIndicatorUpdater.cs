using System;
using System.Threading;
using nanoFramework.Hardware.Esp32;

namespace Nixie_clock_esp32.Nixie
{
	internal class DynamicIndicatorUpdater : IIndicatorUpdater, IDisposable
	{
		#region Fields

		/// <summary>
		/// Дефолтный период обновления 1ms
		/// </summary>
		public static readonly int DefaultUpdatePeriod_ms = 1;

		/// <summary>
		/// Политика данных. Выставляет на шину данных нужный фрагмент сырых данных согласно выбранной группе индикаторов
		/// </summary>
		private readonly IDataPolicy DataPolicy;

		/// <summary>
		/// Селектор групп 
		/// </summary>
		private readonly IIndicatorGroupSelector Selector;

		/// <summary>
		/// Таймер переключения сегментов
		/// </summary>
		private Timer UpdateTimer;

		/// <summary>
		/// Индикация активна?
		/// </summary>
		private bool mEnabled = false;

		/// <summary>
		/// Буфер кадра с сырыми данными
		/// </summary>
		private IRawDataBuffer mData = null;

		#endregion Fields

		#region Constructors

		public DynamicIndicatorUpdater(IDataPolicy datapolicy, IIndicatorGroupSelector selector)
		{
			DataPolicy = datapolicy;
			Selector = selector;
		}

		#endregion Constructors

		#region Methods

		private static void SwitchNextCharacter(object state)
		{
			DynamicIndicatorUpdater _this = (DynamicIndicatorUpdater)state;
			var group = _this.Selector.NextGroup();
			_this.DataPolicy.WriteGroup(_this.Data, group);
		}

		public void Dispose()
		{
			Enabled = false;
		}

		#endregion Methods

		#region Properties

		public bool Enabled
		{
			get => mEnabled;
			set
			{
				if (mEnabled != value)
				{
					if (value)
					{
						UpdateTimer = new Timer(SwitchNextCharacter, this, 0, UpdatePeriod_ms);
					}
					else
					{
						UpdateTimer.Dispose();
					}
					Selector.Enabled = value;
					mEnabled = value;
				}
			}
		}

		public int UpdatePeriod_ms { get; set; } = DefaultUpdatePeriod_ms;

		public IRawDataBuffer Data {
			get => mData;
			set {
				if (mData == null)
				{
					mData = value;
				}
				else
				{
					mData.From(value);
				}
			}
		}

		#endregion Properties
	}
}