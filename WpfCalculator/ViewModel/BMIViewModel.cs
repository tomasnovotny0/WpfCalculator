using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WpfCalculator.Model;
using WpfCalculator.Units;

namespace WpfCalculator.ViewModel
{
    /// <summary>
    /// View model for BMI calculator
    /// </summary>
    class BMIViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Color mappings for all body mass index statuses
        /// </summary>
        private static readonly Dictionary<BMIStatus, Color> statusColorDict;
        /// <summary>
        /// Current background color based on latest BMI value
        /// </summary>
        public Color BackgroundColor { get; private set; } = Colors.White;
        /// <summary>
        /// Description of BMI value
        /// </summary>
        public string ResultText { get; private set; } = "Unknown status";
        /// <summary>
        /// Body mass index value
        /// </summary>
        public string BMI { get; private set; } = "0";
        /// <summary>
        /// List of all length units
        /// </summary>
        public IList<UnitType> LengthUnits { get; }
        /// <summary>
        /// List of all weight units
        /// </summary>
        public IList<UnitType> WeightUnits { get; }
        /// <summary>
        /// Event handler from <see cref="INotifyPropertyChanged"/> interface
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// class constructor, initializes BMI status -> color mappings
        /// </summary>
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
            LengthUnits = UnitManager.GetUnitTypesByCriteria(type => type.Category == UnitCategory.LENGTH);
            WeightUnits = UnitManager.GetUnitTypesByCriteria(type => type.Category == UnitCategory.WEIGHT);
        }

        /// <summary>
        /// Updates properties inside this VM
        /// </summary>
        /// <param name="height">Height value</param>
        /// <param name="weight">Weight value</param>
        public void CalculateBMI(Unit height, Unit weight)
        {
            double bmi = BMICalculator.GetBMI(height, weight);
            BMI = bmi.ToString("0.##"); // format to 2 decimal spaces
            OnPropertyChanged(nameof(BMI));
            BMIStatus status = GetBMIStatus(bmi);
            UpdateColor(status);
            UpdateDisplayedText(status);
        }

        /// <summary>
        /// Called when exception occurs while parsing inputs.
        /// Sets background color to red and update message
        /// </summary>
        /// <param name="message">Message to be shown to user</param>
        public void SetInvalidState(string message)
        {
            BackgroundColor = Colors.Red;
            ResultText = message;
            OnPropertyChanged(nameof(BackgroundColor));
            OnPropertyChanged(nameof(ResultText));
        }

        /// <summary>
        /// Invokes property changed event for specific <paramref name="property"/>
        /// </summary>
        /// <param name="property">Name of property which has changed</param>
        private void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// Updates background color based on BMI status
        /// </summary>
        /// <param name="status">Actual status</param>
        private void UpdateColor(BMIStatus status)
        {
            Color color = statusColorDict[status];
            if (color != BackgroundColor)
            {
                BackgroundColor = color;
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        /// <summary>
        /// Updates information text based on BMI status
        /// </summary>
        /// <param name="status">Actual status</param>
        private void UpdateDisplayedText(BMIStatus status)
        {
            string statusText = $"You are {status.ToString().ToLower()}";
            if (statusText != ResultText)
            {
                ResultText = statusText;
                OnPropertyChanged(nameof(ResultText));
            }
        }

        /// <summary>
        /// Gets BMI status based on value of body mass index
        /// </summary>
        /// <param name="index">Index value to evaluate</param>
        /// <returns><see cref="BMIStatus"/> based on <paramref name="index"/> value</returns>
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

    /// <summary>
    /// Enum containing all body mass index statuses
    /// </summary>
    public enum BMIStatus
    {
        Underweight,
        Healthy,
        Overweight,
        Obese
    }
}
