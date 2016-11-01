// Copyright (c) 2016 All rights reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-11-01</date>

using System;






namespace SapiController.protocol._shared
{
	[Flags]
	public enum LedSet : ushort
	{
		Led0 = 1 << 0,
		Led1 = 1 << 1,
		Led2 = 1 << 2,
		Led3 = 1 << 3,
		Led4 = 1 << 4,
		Led5 = 1 << 5,
		Led6 = 1 << 6,
		Led7 = 1 << 7,
		Led8 = 1 << 8,
		Led9 = 1 << 9,
		Led10 = 1 << 10,
		Led11 = 1 << 11,
		Led12 = 1 << 12,
		Led13 = 1 << 13,
		Led14 = 1 << 14,
		Led15 = 1 << 15,
		All = ushort.MaxValue,
		None = ushort.MinValue,
	}
}