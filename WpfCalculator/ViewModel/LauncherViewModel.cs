using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.ViewModel
{
    class LauncherViewModel
    {
        public LauncherViewModel()
        {
            // set application culture to 'en-US' as that has . character as decimal separator
            CultureInfo en = CultureInfo.GetCultureInfo("en-US");
            CultureInfo.CurrentCulture = en;
            CultureInfo.DefaultThreadCurrentCulture = en;
        }
    }
}
