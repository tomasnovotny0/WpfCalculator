using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfCalculator.Units;
using WpfCalculator.ViewModel;

namespace WpfCalculator.View
{
    /// <summary>
    /// Interaction logic for UnitConverterView.xaml
    /// </summary>
    public partial class UnitConverterView : Window
    {
        public UnitConverterView()
        {
            InitializeComponent();
        }

        private void ConversionValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            UnitConverterViewModel vm = DataContext as UnitConverterViewModel;
            vm.UpdateInput(ConversionValue.Text);
        }

        private void FromUnitBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object selected = FromUnitBox.SelectedItem;
            if (selected is null)
                return;
            UnitType type = selected as UnitType;
            (DataContext as UnitConverterViewModel).UpdateBaseUnit(type, out bool isCompatible);
            if (!isCompatible)
            {
                TargetUnitBox.SelectedIndex = -1;
            }
        }

        private void TargetUnitBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object selected = TargetUnitBox.SelectedItem;
            if (selected is null)
                return;
            UnitType type = selected as UnitType;
            (DataContext as UnitConverterViewModel).UpdateTargetUnit(type);
        }
    }

    
}
