using System;
using System.Windows;
using System.Windows.Data;

namespace AlbumVersionControl.Converters
{
    public class MultiBooleanToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var result = true;

            foreach (var value in values)
            {
                if (value is bool boolValue && !boolValue)
                {
                    result = false;
                    break;
                }
            }

            return (Visibility)(result ? 0 : 2);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}