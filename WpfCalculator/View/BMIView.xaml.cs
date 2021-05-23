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
    /// Interaction logic for BMIView.xaml
    /// </summary>
    public partial class BMIView : Window
    {
        public BMIView()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is BMIViewModel vm)
            {
                try
                {
                    UnitType heightUnitType = HeightUnit.SelectedItem as UnitType;
                    UnitType weightUnitType = WeightUnit.SelectedItem as UnitType;
                    Unit height = heightUnitType.Parse(new StringDataSource(HeightInput.Text));
                    Unit weight = weightUnitType.Parse(new StringDataSource(WeightInput.Text));
                    vm.CalculateBMI(height, weight);
                }
                catch (Exception)
                {
                    vm.SetInvalidState("Invalid input");
                }
            }
        }
    }
}
