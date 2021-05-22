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
                UnitType heightUnitType = UnitManager.FindUnitByKey(HeightUnit.Text);
                UnitType weightUnitType = UnitManager.FindUnitByKey(WeightUnit.Text);
                Unit height = heightUnitType.Parse(new StringDataSource(HeightInput.Text), UnitManager.METER);
                Unit weight = weightUnitType.Parse(new StringDataSource(WeightInput.Text), UnitManager.KILOGRAM);
                vm.CalculateBMI(height, weight);
            }
        }
    }
}
