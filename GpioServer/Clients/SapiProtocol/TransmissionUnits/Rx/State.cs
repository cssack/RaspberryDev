// Copyright (c) 2016 All rights reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-11-02</date>

using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using CsWpfBase.Ev.Objects;
using PowerPiProtocol.TransmissionUnits._shared;






namespace PowerPiProtocol.TransmissionUnits.Rx
{
	/// <summary>Contains information about the target.</summary>
	public class State : Base
	{
		/// <summary>The byte which needs to be sent to server in order to know what should happen next.</summary>
		private static byte RequestIdentifier = 0;


		private DateTime _lastRead;
		private PinSet _pinSet;
		private TimeSpan _runtime;
		private double _temperature;

		internal State()
		{

		}

		///<summary>The time the OS of the target is running.</summary>
		public TimeSpan Runtime
		{
			get { return _runtime; }
			private set{SetProperty(ref _runtime, value);}
		}
		///<summary>The temperature of the target CPU in degree.</summary>
		public double Temperature
		{
			get { return _temperature; }
			private set { SetProperty(ref _temperature, value); }
		}
		///<summary>The current state of the GPIOs of the target.</summary>
		public PinSet PinSet
		{
			get { return _pinSet; }
			private set { SetProperty(ref _pinSet, value); }
		}
		///<summary>Returns the time when the last successful read happened</summary>
		public DateTime LastRead
		{
			get { return _lastRead; }
			private set
			{
				if (!SetProperty(ref _lastRead, value)) return;
				OnPropertyChanged(nameof(HasBeenLoaded));
			}
		}
		///<summary>returns true if any data has been loaded.</summary>
		public bool HasBeenLoaded => LastRead != DateTime.MinValue;


		internal void SendRequest(BinaryWriter writer)
		{
			writer.Write(RequestIdentifier);
		}

		/// <summary>reads the current state from an connection.</summary>
		/// <param name="reader">the reader associated with the connection</param>
		internal void ReceiveResponse(BinaryReader reader)
		{
			LastRead = DateTime.Now;
			Temperature = reader.ReadInt32() / 1000.0;
			Runtime = TimeSpan.FromMilliseconds(reader.ReadInt32());
			PinSet = (PinSet) reader.ReadUInt16();

			Application.Current.Dispatcher.BeginInvoke(new Action(() =>
						{
							OnPropertyChanged(nameof(Runtime));
						}), DispatcherPriority.Background);
		}
	}
}