// Copyright (c) 2016 All rights reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-11-01</date>

using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Themes.Controls.Containers;






namespace SapiController
{
	/// <summary>Interaction logic for MainWindow.xaml</summary>
	public partial class MainWindow : CsWindow
	{

		/// <summary>The <see cref="DependencyProperty"/> for the <see cref="Connection" /> property.</summary>
		public static readonly DependencyProperty ConnectionProperty = DependencyProperty.Register("Connection", typeof(SapiConnection), typeof(MainWindow), new FrameworkPropertyMetadata
		{
			//PropertyChangedCallback = (o, args) => ((MainWindow)o).ConnectionDpChanged((SapiConnection)args.OldValue, (SapiConnection)args.NewValue),
			DefaultValue = default(SapiConnection),
			DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
		});

		/// <summary>The SAPI Connection</summary>
		public SapiConnection Connection
		{
			get { return (SapiConnection) GetValue(ConnectionProperty); }
			set { SetValue(ConnectionProperty, value); }
		}
		public MainWindow()
		{
			Connection = new SapiConnection("sapi", 12345);
			InitializeComponent();
		}


		private void Load(object sender, RoutedEventArgs e)
		{
			try
			{
				Connection.DownloadDetails();
			}
			catch (Exception)
			{

			}
		}



		
	}
}