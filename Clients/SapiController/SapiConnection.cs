// Copyright (c) 2016 All rights reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-11-01</date>

using System;
using System.IO;
using System.Net.Sockets;
using CsWpfBase.Ev.Objects;
using SapiController.protocol;
using SapiController.protocol._shared;






namespace SapiController
{
	/// <summary>Manages a SAPI server.</summary>
	public class SapiConnection : Base
	{
		private SapiGpio _gpio;
		private string _host;
		private int _port;
		private SapiState _state;


		public SapiConnection(string host, int port)
		{
			Host = host;
			Port = port;
		}

		///<summary>The hostname or ip address of the target host</summary>
		public string Host
		{
			get { return _host; }
			private set { SetProperty(ref _host, value); }
		}
		///<summary>The port of the target host.</summary>
		public int Port
		{
			get { return _port; }
			private set { SetProperty(ref _port, value); }
		}
		///<summary>Gets the initial data of the SAPI.</summary>
		public SapiState State
		{
			get { return _state; }
			private set { SetProperty(ref _state, value); }
		}
		///<summary>Gets the gpio configuration</summary>
		public SapiGpio Gpio
		{
			get { return _gpio; }
			private set { SetProperty(ref _gpio, value); }
		}

		/// <summary>Downloads the details of the sapi controller.</summary>
		public void DownloadDetails()
		{
			Do((writer, reader) =>
			{
				writer.Write(SapiState.RequestByte);
				if (State == null)
					State = new SapiState();
				State.Read(reader);


				if (Gpio == null)
					Gpio = new SapiGpio();
				Gpio.Interpret(State.GpioState);
			});
		}




		private void Do(Action<BinaryWriter, BinaryReader> action)
		{
			TcpClient client = null;
			try
			{
				client = new TcpClient(Host, Port);
				using (var stream = client.GetStream())
				{
					var writer = new BinaryWriter(stream);
					var reader = new BinaryReader(stream);

					action(writer, reader);

					stream.Flush();
				}
			}
			finally
			{
				client?.Close();
			}
		}
	}
}