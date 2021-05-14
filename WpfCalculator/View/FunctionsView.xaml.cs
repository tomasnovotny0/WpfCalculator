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
using WpfCalculator.Math;
using WpfCalculator.ViewModel;

namespace WpfCalculator.View
{
    /// <summary>
    /// Interaction logic for FunctionsView.xaml
    /// </summary>
    public partial class FunctionsView : Window
    {
        private readonly TextBox inputBlock;

        public FunctionsView(TextBox _inputBlock)
        {
            InitializeComponent();
            inputBlock = _inputBlock;
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            object selectedFunction = FunctionSelector.SelectedItem;
            if (selectedFunction is Function function)
            {
                inputBlock.Text += function.FunctionSignature;
            }
            Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SelectFunction(Function function)
        {
            ((FunctionsViewModel)DataContext).SelectFunction(function);
        }

        private void FunctionSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object added = e.AddedItems[0];
            if (added is Function f)
            {
                SelectFunction(f);
            }
        }
    }
}
