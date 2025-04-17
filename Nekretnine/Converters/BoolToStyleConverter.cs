using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Nekretnine.Converters
{
    public class BoolToStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool canSend = (bool)value;
            var styles = parameter as Style[];

            if (styles == null || styles.Length != 2)
                throw new ArgumentException("Converter parameter must be an array with exactly two styles");

            return canSend ? styles[0] : styles[1];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
