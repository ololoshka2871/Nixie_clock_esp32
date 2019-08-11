using System.Collections;

namespace Nixie_clock_esp32.Nixie
{
	internal class ByteDataBuffer : IRawDataBuffer
	{
		#region Fields

		private readonly byte[] Data;

		#endregion Fields

		#region Constructors

		public ByteDataBuffer(int size)
		{
			Data = new byte[size];
		}

		public ByteDataBuffer(ICollection initialData)
		{
			Data = new byte[initialData.Count];
			var iterator = initialData.GetEnumerator();
			for (int i = 0; i < Data.Length; ++i)
			{
				Data[i] = (byte)iterator.Current;
				iterator.MoveNext();
			}
		}

		#endregion Constructors

		#region Methods

		public void From(IRawDataBuffer newstate)
		{
			for (int i = 0; i < Data.Length; ++i)
			{
				Data[i] = (byte)newstate.Get(i);
			}
		}

		public uint Get(int element_number)
			=> (element_number < Data.Length)
				? Data[element_number]
				: 0u;

		public IRawDataBuffer Set(int element_number, uint value)
		{
			if (element_number < Data.Length)
			{
				Data[element_number] = (byte)value;
			}
			return this;
		}

		#endregion Methods
	}
}