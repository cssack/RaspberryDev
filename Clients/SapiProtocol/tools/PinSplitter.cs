// Copyright (c) 2016 All rights reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-11-02</date>

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CsWpfBase.Ev.Objects;
using PowerPiProtocol.TransmissionUnits.Tx;
using PowerPiProtocol.TransmissionUnits._shared;






namespace PowerPiProtocol.tools
{
	/// <summary>Used for accessing pins on @ each time.</summary>
	public sealed class PinSplitter : Base
	{
		private int _changedPins = 0;
		private bool _hasBeenChanged;
		private ReadOnlyObservableCollection<SplittedPin> _items;
		private ObservableCollection<SplittedPin> _writeableItems;
		private ObservableCollection<SplittedPin> WriteableItems => _writeableItems ?? (_writeableItems = new ObservableCollection<SplittedPin>());

		public PinSplitter()
		{
			InitItems();
		}

		///<summary>The single pins in the according wrapper functions</summary>
		public ReadOnlyObservableCollection<SplittedPin> Items => _items ?? (_items = new ReadOnlyObservableCollection<SplittedPin>(WriteableItems));

		/// <summary>True if any of the <see cref="Items" /> has been changed.</summary>
		public bool HasBeenChanged
		{
			get { return _hasBeenChanged; }
			private set { SetProperty(ref _hasBeenChanged, value); }
		}

		private void InitItems()
		{
			foreach (var pin in Enum.GetValues(typeof(PinSet)).Cast<ushort>().Select(x => (PinSet) x))
			{
				if ((pin == PinSet.All) || (pin == PinSet.None))
					continue;

				var entry = new SplittedPin(pin);
				entry.SetOriginalValue(false);
				entry.PropertyChanged += Entry_PropertyChanged;
				WriteableItems.Add(entry);
			}
		}

		private void Entry_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != nameof(SplittedPin.HasBeenChanged))
				return;

			_changedPins += ((SplittedPin)sender).HasBeenChanged ? 1 : -1;
			HasBeenChanged = _changedPins != 0;
		}

		internal void ApplyValue(PinSet set)
		{
			foreach (var pin in Enum.GetValues(typeof(PinSet)).Cast<ushort>().Select(x => (PinSet) x))
			{
				if ((pin == PinSet.All) || (pin == PinSet.None))
					continue;

				WriteableItems.First(x => x.Pin == pin).SetOriginalValue(set.HasFlag(pin));
			}
		}

		internal ApplyGpio GetCurrentChangeSet()
		{
			int value = 0, mask = 0;
			foreach (var splittedPin in Items)
			{
				splittedPin.Apply(ref value, ref mask);
			}
			return new ApplyGpio
			{
				Value = (PinSet) value,
				Mask = (PinSet) mask
			};
		}
	}
}