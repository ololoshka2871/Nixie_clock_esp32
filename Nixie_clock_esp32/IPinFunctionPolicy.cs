namespace Nixie_clock_esp32
{
	internal interface IPinFunctionPolicy
	{
		#region Methods

		/// <summary>
		/// Set pin(s) alternative function
		/// </summary>
		void SetPinAlternate();

		#endregion Methods
	}
}