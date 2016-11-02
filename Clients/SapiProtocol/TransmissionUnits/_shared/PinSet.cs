// Copyright (c) 2016 All rights reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-11-02</date>

using System;
using CsWpfBase.Ev.Attributes;






namespace PowerPiProtocol.TransmissionUnits._shared
{
	/// <summary>Contains all possible pins on the remote targer.</summary>
	[Flags]
	public enum PinSet : ushort
	{
		///<summary>[PHY: 7], [WPI: 7]</summary>
		[EnumDescription("WPI-7", "[PHY: 7], [WPI: 7]")] Pin7 = 1 << 0,
		///<summary>[PHY: 11], [WPI: 0]</summary>
		[EnumDescription("WPI-0", "[PHY: 11], [WPI: 0]")] Pin11 = 1 << 1,
		///<summary>[PHY: 12], [WPI: 1]</summary>
		[EnumDescription("WPI-1", "[PHY: 12], [WPI: 1]")] Pin12 = 1 << 2,
		///<summary>[PHY: 13], [WPI: 2]</summary>
		[EnumDescription("WPI-2", "[PHY: 13], [WPI: 2]")] Pin13 = 1 << 3,
		///<summary>[PHY: 15], [WPI: 3]</summary>
		[EnumDescription("WPI-3", "[PHY: 15], [WPI: 3]")] Pin15 = 1 << 4,
		///<summary>[PHY: 16], [WPI: 4]</summary>
		[EnumDescription("WPI-4", "[PHY: 16], [WPI: 4]")] Pin16 = 1 << 5,
		///<summary>[PHY: 18], [WPI: 5]</summary>
		[EnumDescription("WPI-5", "[PHY: 18], [WPI: 5]")] Pin18 = 1 << 6,
		///<summary>[PHY: 22], [WPI: 6]</summary>
		[EnumDescription("WPI-6", "[PHY: 22], [WPI: 6]")] Pin22 = 1 << 7,
		///<summary>[PHY: 29], [WPI: 21]</summary>
		[EnumDescription("WPI-21", "[PHY: 29], [WPI: 21]")] Pin29 = 1 << 8,
		///<summary>[PHY: 31], [WPI: 22]</summary>
		[EnumDescription("WPI-22", "[PHY: 31], [WPI: 22]")] Pin31 = 1 << 9,
		///<summary>[PHY: 32], [WPI: 26]</summary>
		[EnumDescription("WPI-26", "[PHY: 32], [WPI: 26]")] Pin32 = 1 << 10,
		///<summary>[PHY: 33], [WPI: 23]</summary>
		[EnumDescription("WPI-23", "[PHY: 33], [WPI: 23]")] Pin33 = 1 << 11,
		///<summary>[PHY: 35], [WPI: 24]</summary>
		[EnumDescription("WPI-24", "[PHY: 35], [WPI: 24]")] Pin35 = 1 << 12,
		///<summary>[PHY: 36], [WPI: 27]</summary>
		[EnumDescription("WPI-27", "[PHY: 36], [WPI: 27]")] Pin36 = 1 << 13,
		///<summary>[PHY: 37], [WPI: 25]</summary>
		[EnumDescription("WPI-25", "[PHY: 37], [WPI: 25]")] Pin37 = 1 << 14,
		///<summary>[PHY: 38], [WPI: 28]</summary>
		[EnumDescription("WPI-28", "[PHY: 38], [WPI: 28]")] Pin38 = 1 << 15,

		/// <summary>All pins set.</summary>
		All = ushort.MaxValue,
		/// <summary>No pins set.</summary>
		None = ushort.MinValue,
	}



	internal static class PinSetExtensions
	{
		public static int GetShifter(this PinSet pin)
		{
			int pVal = (ushort) pin;
			if (pVal == 0)
				return 0;

			var shifts = 0;
			while ((pVal & 1) != 1)
			{
				pVal = pVal >> 1;
				shifts++;
			}
			return shifts;
		}
	}
}