using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WpfCalculator.Model;

namespace WpfCalculator.ViewModel
{
    class BMIViewModel : INotifyPropertyChanged
    {
        private static readonly Dictionary<BMIStatus, Color> statusColorDict;
        public Color BackgroundColor { get; private set; } = Colors.White;
        public string ResultText { get; private set; } = "Unknown status";
        public string BMI { get; private set; } = "0";
        public event PropertyChangedEventHandler PropertyChanged;

        static BMIViewModel()
        {
            statusColorDict = new Dictionary<BMIStatus, Color>
            {
                [BMIStatus.Underweight] = Colors.Aqua,
                [BMIStatus.Healthy] = Color.FromRgb(60, 255, 60),
                [BMIStatus.Overweight] = Colors.Orange,
                [BMIStatus.Obese] = Colors.Red
            };
        }

        public BMIViewModel()
        {
        }

        // TODO replace with units
        public void CalculateBMI(double height, double weight)
        {
            double bmi = BMICalculator.GetBMI(height, weight);
            BMI = bmi.ToString("0.##");
            OnPropertyChanged(nameof(BMI));
            BMIStatus status = GetBMIStatus(bmi);
            UpdateColor(status);
            UpdateDisplayedText(status);
        }

        private void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private void UpdateColor(BMIStatus status)
        {
            Color color = statusColorDict[status];
            if (color != BackgroundColor)
            {
                BackgroundColor = color;
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        private void UpdateDisplayedText(BMIStatus status)
        {
            string statusText = $"You are {status.ToString().ToLower()}";
            if (statusText != ResultText)
            {
                ResultText = statusText;
                OnPropertyChanged(nameof(ResultText));
            }
        }

        public static BMIStatus GetBMIStatus(double index)
        {
            if (index < 18.5)
                return BMIStatus.Underweight;
            if (index < 25)
                return BMIStatus.Healthy;
            if (index < 30)
                return BMIStatus.Overweight;
            return BMIStatus.Obese;
        }
    }

    public enum BMIStatus
    {
        Underweight,
        Healthy,
        Overweight,
        Obese
    }
}
