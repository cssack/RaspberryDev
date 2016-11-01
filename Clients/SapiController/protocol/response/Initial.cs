// Copyright (c) 2016 All rights reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-11-01</date>

using System;
using SapiController.protocol._shared;






namespace SapiController.protocol.response
{
	public struct Initial
	{
		public string SapiName { get; set; }
		public int Temperature { get; set; }
		public int Runtime { get; set; }
		public LedSet CurrentLedState { get; set; }
	}
}