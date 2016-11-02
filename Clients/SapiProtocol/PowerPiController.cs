// Copyright (c) 2016 All rights reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-11-02</date>

using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using CsWpfBase.Ev.Objects;
using PowerPiProtocol.tools;
using PowerPiProtocol.TransmissionUnits.Rx;






namespace PowerPiProtocol
{
	/// <summary>Used to interact with a Sapi</summary>
	public class PowerPiController : Base
	{
		private PinSplitter _gpio = new PinSplitter();
		private string _hostname;
		private State _latestState = new State();
		private int _port;

		public PowerPiController(string hostname) : this(hostname, 12345)
		{
		}

		public PowerPiController(string hostname, int port)
		{
			_hostname = hostname;
			_port = port;
		}

		///<summary>The hostname of the target.</summary>
		public string Hostname
		{
			get { return _hostname; }
			private set { SetProperty(ref _hostname, value); }
		}
		///<summary>The port of the target.</summary>
		public int Port
		{
			get { return _port; }
			private set { SetProperty(ref _port, value); }
		}
		///<summary>The latest downloaded state.</summary>
		public State LatestState => _latestState ?? (_latestState = new State());
		///<summary>The gpio configurator</summary>
		public PinSplitter Gpio => _gpio ?? (_gpio = new PinSplitter());

		/// <summary>Reloads the <see cref="LatestState" />.</summary>
		public Task ReloadState()
		{
			return Do((writer, reader) =>
			{
				LatestState.SendRequest(writer);
				ReceiveState(reader);
			});
		}

		/// <summary>Sends the changed gpios and receives a new state</summary>
		public Task SendGpio()
		{
			return Do((writer, reader) =>
			{
				Gpio.GetCurrentChangeSet().SendRequest(writer);
				ReceiveState(reader);
			});
		}

		private void ReceiveState(BinaryReader reader)
		{
			LatestState.ReceiveResponse(reader);
			Gpio.ApplyValue(LatestState.PinSet);
		}

		private Task Do(Action<BinaryWriter, BinaryReader> action)
		{
			var task = new Task(() =>
			{
				TcpClient client = null;
				try
				{
					client = new TcpClient(Hostname, Port);
					using(var stream = client.GetStream())
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
			}, TaskCreationOptions.LongRunning);
			task.Start(TaskScheduler.Default);
			return task;
		}
	}
}