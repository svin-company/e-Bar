using eBar.Core.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace eBar.Core.ParameterConverter
{
    public class ConfirmParametersConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new ConfirmParameters
            {
                Table = values[0] as Table,
                Waiter = values[1] as Waiter,
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
