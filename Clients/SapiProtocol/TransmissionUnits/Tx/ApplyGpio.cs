// Copyright (c) 2016 All rights reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-11-02</date>

using System;
using System.IO;
using CsWpfBase.Ev.Objects;
using PowerPiProtocol.TransmissionUnits._shared;






namespace PowerPiProtocol.TransmissionUnits.Tx
{
	internal class ApplyGpio : Base
	{
		private static readonly byte RequestIdentifier = 1;
		private PinSet _mask;
		private PinSet _value;

		///<summary>The value which should be set on the target.</summary>
		public PinSet Value
		{
			get { return _value; }
			set { SetProperty(ref _value, value); }
		}
		///<summary>The mask, similar to network mask</summary>
		public PinSet Mask
		{
			get { return _mask; }
			set { SetProperty(ref _mask, value); }
		}

		public void SendRequest(BinaryWriter writer)
		{
			writer.Write(RequestIdentifier);
			writer.Write((ushort) Value);
			writer.Write((ushort) Mask);
		}
	}
}