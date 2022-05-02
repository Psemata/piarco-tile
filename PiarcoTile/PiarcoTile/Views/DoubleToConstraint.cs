using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PiarcoTile.Views {
    class DoubleToConstraint : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return Constraint.Constant((double)value);
        }

        /// <summary>
        /// Incorrect, but not used for the projet, need to be implemeted due to the usage of the IValueConverter interface
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return value;
        }
    }
}
