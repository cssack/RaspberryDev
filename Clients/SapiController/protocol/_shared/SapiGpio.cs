// Copyright (c) 2016 All rights reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-11-01</date>

using System;
using System.Collections.ObjectModel;
using System.Linq;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys;






namespace SapiController.protocol._shared
{
	public class SapiGpio : Base
	{
		public static byte RequestByte = 1;
		private ReadOnlyObservableCollection<Entry> _pins;
		private ObservableCollection<Entry> _writeablePins;
		private ObservableCollection<Entry> WriteablePins => _writeablePins ?? (_writeablePins = new ObservableCollection<Entry>());



		///<summary>Contains a list of all pins</summary>
		public ReadOnlyObservableCollection<Entry> Pins => _pins ?? (_pins = new ReadOnlyObservableCollection<Entry>(WriteablePins));



		public void Interpret(IoPins gpio)
		{
			foreach (var pin in Enum.GetValues(typeof(IoPins)).Cast<ushort>().Select(x => (IoPins) x))
			{
				if ((pin == IoPins.All) || (pin == IoPins.None))
					continue;

				var entry = WriteablePins.FirstOrDefault(x => x.Pin == pin);
				if (entry == null)
				{
					entry = new Entry(pin);
					WriteablePins.Add(entry);
				}
				entry.SetOriginalValue(gpio.HasFlag(pin));
			}
		}



		public class Entry : Base
		{
			private readonly ProcessLock _lock = new ProcessLock();
			private string _alias;
			private bool _originalValue;
			private IoPins _pin;
			private bool _value;

			public Entry(IoPins pin)
			{
				Pin = pin;
				Alias = pin.GetName();
			}


			///<summary>The associated pin</summary>
			public IoPins Pin
			{
				get { return _pin; }
				set { SetProperty(ref _pin, value); }
			}
			///<summary>the name of the gpio pin.</summary>
			public string Alias
			{
				get { return _alias; }
				set { SetProperty(ref _alias, value); }
			}
			///<summary>The value which was obtained by the current state.</summary>
			public bool OriginalValue
			{
				get { return _originalValue; }
				set
				{
					if (!SetProperty(ref _originalValue, value))
						return;
					OnPropertyChanged(nameof(HasChanged));
				}
			}
			///<summary>The value of the pin</summary>
			public bool Value
			{
				get { return _value; }
				set
				{
					if (!SetProperty(ref _value, value)) return;
					OnPropertyChanged(nameof(HasChanged));
				}
			}
			///<summary>True if value differs from original value.</summary>
			public bool HasChanged => Value != OriginalValue;


			internal void SetOriginalValue(bool value)
			{
				using (_lock.Activate())
				{
					Value = value;
					OriginalValue = value;
				}
			}
		}
	}
}