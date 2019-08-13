namespace Nixie_clock_esp32.Nixie
{
	internal class ID1DataEncoder : IEncoder
	{
		#region Fields

		public const uint bitmask = 0x0f;

		public const int DataBitsPreIndicator = 4;

		private readonly EncodeSimbol EncodeDelegate;

		#endregion Fields

		#region Constructors

		public ID1DataEncoder(EncodeSimbol encodeDelegate)
		{
			EncodeDelegate = encodeDelegate;
		}

		#endregion Constructors

		#region Delegates

		public delegate uint EncodeSimbol(char simbol);

		#endregion Delegates

		#region Methods

		public uint[] Encode(string input)
		{
			var res = new uint[input.Length];
			for (int i = 0; i < input.Length; ++i)
			{
				res[i] = EncodeDelegate(input[i]) & bitmask;
			}
			return res;
		}

		#endregion Methods
	}
}