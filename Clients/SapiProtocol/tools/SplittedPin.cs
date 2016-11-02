// Copyright (c) 2016 All rights reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-11-02</date>

using System;
using CsWpfBase.Ev.Objects;
using PowerPiProtocol.TransmissionUnits._shared;






namespace PowerPiProtocol.tools
{
	/// <summary>Represents on pin which can be manipulated.</summary>
	public sealed class SplittedPin : Base
	{
		private readonly int _shifter;
		private bool _originalValue;
		private bool _value;

		internal SplittedPin(PinSet pin)
		{
			Pin = pin;
			_shifter = pin.GetShifter();
		}

		///<summary>The target pin on the remote target.</summary>
		public PinSet Pin { get; }
		///<summary>The value of the pin this can be changed.</summary>
		public bool Value
		{
			get { return _value; }
			set
			{
				if (!SetProperty(ref _value, value)) return;
				OnPropertyChanged(nameof(HasBeenChanged));
			}
		}
		///<summary>The value which was determined by the last state</summary>
		public bool OriginalValue
		{
			get { return _originalValue; }
			private set
			{
				if (!SetProperty(ref _originalValue, value)) return;
				OnPropertyChanged(nameof(HasBeenChanged));
			}
		}

		/// <summary>returns true if the <see cref="Value" /> is not equal to the <see cref="OriginalValue" /></summary>
		public bool HasBeenChanged => Value != OriginalValue;


		internal void SetOriginalValue(bool value)
		{
			OriginalValue = value;
			Value = value;
		}
		internal void Apply(ref int value, ref int mask)
		{
			if (!HasBeenChanged)
				return;
			var maskbit = 1 << _shifter;
			mask = mask | maskbit;

			if (!Value)
				return;

			value = value | maskbit;
		}
	}
}