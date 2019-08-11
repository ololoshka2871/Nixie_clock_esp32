using System;

namespace Nixie_clock_esp32.Nixie
{
	internal class ID1Datapolicy : IDataPolicy
	{
		#region Fields

		/// <summary>
		/// Количество битов для каждой микросхемы 155ИД1
		/// </summary>
		public static readonly int DataBitsPreChannel = 4;

		/// <summary>
		/// Количество одновременно подключенных микросхем 155ИД1 к шине данных
		/// </summary>
		private readonly int Channels;

		/// <summary>
		/// Шина данных (выход)
		/// </summary>
		private readonly IOutDataPort DataPort;

		#endregion Fields

		#region Constructors

		public ID1Datapolicy(IOutDataPort dataport)
		{
			Channels = dataport.Width / DataBitsPreChannel;
			if (dataport.Width % DataBitsPreChannel != 0)
			{
				throw new ArgumentException("Dataport width mast be multiply of 4");
			}

			DataPort = dataport;
		}

		#endregion Constructors

		#region Methods

		public void WriteGroup(IRawDataBuffer rawdata, int group_number)
		{
			uint dataPortNewValue = 0;
			for (int ch = 0; ch < Channels; ++ch)
			{
				uint value = rawdata.Get(ch * group_number);
				dataPortNewValue |= value << (DataBitsPreChannel * ch);
			}
			DataPort.Value = dataPortNewValue;
		}

		#endregion Methods
	}
}