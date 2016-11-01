// Copyright (c) 2016 All rights reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-11-01</date>

using System;
using System.IO;
using CsWpfBase.Ev.Objects;
using SapiController.protocol._shared;






namespace SapiController.protocol
{
	/// <summary>Used for state recogition of the SAPI</summary>
	public class SapiState : Base
	{
		public static byte RequestByte = 0;


		private GpioPins _gpioState;
		private TimeSpan _runtime;
		private int _temperature;



		///<summary>The current temperature of the SAPI in milli degree.</summary>
		public int Temperature
		{
			get { return _temperature; }
			private set { SetProperty(ref _temperature, value); }
		}
		///<summary>The time of the process running this service on the SAPI.</summary>
		public TimeSpan Runtime
		{
			get { return _runtime; }
			private set { SetProperty(ref _runtime, value); }
		}
		///<summary>The current GPIO state of the SAPI.</summary>
		public GpioPins GpioState
		{
			get { return _gpioState; }
			private set { SetProperty(ref _gpioState, value); }
		}

		public void Read(BinaryReader rd)
		{
			Temperature = rd.ReadInt32();
			Runtime = TimeSpan.FromMilliseconds(rd.ReadInt32());
			GpioState = (GpioPins) rd.ReadUInt16();
		}
	}
}