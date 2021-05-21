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
                vm.CalculateBMI(double.Parse(HeightInput.Text), double.Parse(WeightInput.Text));
            }
        }
    }
}
