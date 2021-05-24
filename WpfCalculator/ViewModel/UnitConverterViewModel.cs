﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfCalculator.Units;

namespace WpfCalculator.ViewModel
{
    public class UnitConverterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public double ConversionRate { get; private set; }
        public double Result { get; private set; }
        public IList<UnitType> AvailableUnitTypes { get; }
        public IList<UnitType> CompatibleUnitTypes { get; }
        private UnitType BaseUnit { get; set; }
        private UnitType TargetUnit { get; set; }

        public UnitConverterViewModel()
        {
            ConversionRate = 0;
            Result = 0;
            AvailableUnitTypes = UnitManager.GetUnitTypesByCriteria(type => true);
        }

        public void UpdateInput(string input)
        {
            if (double.TryParse(input, out double value))
            {
                Result = ConversionRate * value;
            }
        }

        public void UpdateBaseUnit(UnitType baseUnit, out bool isCompatibleWithTarget)
        {
            isCompatibleWithTarget = true;
            BaseUnit = baseUnit;
            // update conversion rate and output
        }

        public void UpdateTargetUnit(UnitType targetUnit)
        {
            TargetUnit = targetUnit;
            // update conversion rate and output
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
