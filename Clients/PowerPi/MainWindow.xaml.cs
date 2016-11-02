// Copyright (c) 2016 All rights reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-11-02</date>

using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global;
using CsWpfBase.Themes.Controls.Containers;
using PowerPiProtocol;






namespace PowerPi
{
	/// <summary>Interaction logic for MainWindow.xaml</summary>
	public partial class MainWindow : CsWindow
	{
		#region DP Keys
		/// <summary>The <see cref="DependencyProperty" /> for the <see cref="PowerPi" /> property.</summary>
		public static readonly DependencyProperty PowerPiProperty = DependencyProperty.Register("PowerPi", typeof(PowerPiController), typeof(MainWindow), new FrameworkPropertyMetadata
		{
			BindsTwoWayByDefault = true,
			//PropertyChangedCallback = (o, args) => ((MainWindow)o).PowerPiDpChanged((PowerPiController)args.OldValue, (PowerPiController)args.NewValue),
			DefaultValue = default(PowerPiController),
			DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
		});
		#endregion


		public MainWindow()
		{
			InitPowerPi();
			InitializeComponent();
		}

		/// <summary>The currently used power pi</summary>
		public PowerPiController PowerPi
		{
			get { return (PowerPiController) GetValue(PowerPiProperty); }
			set { SetValue(PowerPiProperty, value); }
		}


		private void InitPowerPi()
		{
			PowerPi = new PowerPiController("Sapi");
		}

		private void Load(object sender, RoutedEventArgs e)
		{
			var loading = ((GlyphIconButton)sender).StartLoading();

			PowerPi.ReloadState().ContinueWith(t =>
					{
						loading.Dispose();
						if (t.Exception != null)
							CsGlobal.Message.Push(t.Exception.MostInner());
					}, TaskScheduler.FromCurrentSynchronizationContext());
		}

		private void Upload(object sender, RoutedEventArgs e)
		{
			var loading = ((GlyphIconButton)sender).StartLoading();

			PowerPi.SendGpio().ContinueWith(t =>
			{
				loading.Dispose();
				if(t.Exception != null)
					CsGlobal.Message.Push(t.Exception.MostInner());
			}, TaskScheduler.FromCurrentSynchronizationContext());
		}
	}
}