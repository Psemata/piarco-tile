using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PiarcoTile.Views {
    /// <summary>
    /// Convert double values to Constraints
    /// </summary>
    class DoubleToConstraint : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return Constraint.Constant((double)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
