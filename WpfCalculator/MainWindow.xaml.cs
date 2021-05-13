using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfCalculator.Math;

namespace WpfCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button0_Click(object sender, RoutedEventArgs e)
        {
            InsertStringToTextBox("0");
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            InsertStringToTextBox("1");
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            InsertStringToTextBox("2");
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            InsertStringToTextBox("3");
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            InsertStringToTextBox("4");
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            InsertStringToTextBox("5");
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            InsertStringToTextBox("6");
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            InsertStringToTextBox("7");
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            InsertStringToTextBox("8");
        }

        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            InsertStringToTextBox("9");
        }

        private void ButtonModulo_Click(object sender, RoutedEventArgs e)
        {
            InsertOperatorToTextBox(Operators.MODULO);
        }

        private void ButtonPwr_Click(object sender, RoutedEventArgs e)
        {
            InsertOperatorToTextBox(Operators.POWER);
        }

        private void ButtonPwr2_Click(object sender, RoutedEventArgs e)
        {
            InsertOperatorToTextBox(Operators.POWER);
            InsertStringToTextBox("2");
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            if (InputTextBox.Text.Length > 0)
                InputTextBox.Text = InputTextBox.Text.Substring(0, InputTextBox.Text.Length - 1);
        }

        private void ButtonRightBracket_Click(object sender, RoutedEventArgs e)
        {
            InsertStringToTextBox("(");
        }

        private void ButtonLeftBracket_Click(object sender, RoutedEventArgs e)
        {
            InsertStringToTextBox(")");
        }

        private void ButtonSqrt_Click(object sender, RoutedEventArgs e)
        {
            InsertFunctionToTextBox(Functions.SQRT);
        }

        private void ButtonDivide_Click(object sender, RoutedEventArgs e)
        {
            InsertOperatorToTextBox(Operators.DIVIDE);
        }

        private void ButtonMultiply_Click(object sender, RoutedEventArgs e)
        {
            InsertOperatorToTextBox(Operators.MULTIPLY);
        }

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            InsertOperatorToTextBox(Operators.SUBTRACT);
        }

        private void ButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            InsertOperatorToTextBox(Operators.ADD);
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            InputTextBox.Text = "";
        }

        private void ButtonDecSeparator_Click(object sender, RoutedEventArgs e)
        {
            InsertStringToTextBox(".");
        }

        private void InsertOperatorToTextBox(Operator @operator)
        {
            InsertStringToTextBox(@operator.OperatorCharacter.ToString());
        }

        private void InsertFunctionToTextBox(Function function)
        {
            InsertStringToTextBox(function.ToString());
        }

        private void InsertStringToTextBox(string textToInsert)
        {
            InputTextBox.Text += textToInsert;
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((MainViewModel)DataContext).ParseCalculatorInput(InputTextBox.Text);
        }

        private void ButtonPi_Click(object sender, RoutedEventArgs e)
        {
            InsertFunctionToTextBox(Functions.PI);
        }
    }
}
