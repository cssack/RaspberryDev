// Copyright (c) 2016 All rights reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-11-01</date>

using System;
using CsWpfBase.Ev.Attributes;






namespace SapiController.protocol._shared
{
	[Flags]
	public enum IoPins : ushort
	{
		[EnumDescription("Physical: 11")] Pin0 = 1 << 0,
		[EnumDescription("Physical: 12")] Pin1 = 1 << 1,
		[EnumDescription("Physical: 13")] Pin2 = 1 << 2,
		[EnumDescription("Physical: 15")] Pin3 = 1 << 3,
		[EnumDescription("Physical: 16")] Pin4 = 1 << 4,
		[EnumDescription("Physical: 18")] Pin5 = 1 << 5,
		[EnumDescription("Physical: 22")] Pin6 = 1 << 6,
		[EnumDescription("Physical: 7")] Pin7 = 1 << 7,
		[EnumDescription("Physical: 29")] Pin8 = 1 << 8,
		[EnumDescription("Physical: 31")] Pin9 = 1 << 9,
		[EnumDescription("Physical: 32")] Pin10 = 1 << 10,
		[EnumDescription("Physical: 33")] Pin11 = 1 << 11,
		[EnumDescription("Physical: 35")] Pin12 = 1 << 12,
		[EnumDescription("Physical: 36")] Pin13 = 1 << 13,
		[EnumDescription("Physical: 37")] Pin14 = 1 << 14,
		[EnumDescription("Physical: 38")] Pin15 = 1 << 15,
		All = ushort.MaxValue,
		None = ushort.MinValue,
	}



	public static class IoPinsExtensions
	{
		public static int GetShifter(this IoPins pin)
		{
			return Convert.ToInt32(pin.ToString().Substring(3));
		}
	}
}