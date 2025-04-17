using System.Globalization;
using System.Windows.Data;

namespace Nekretnine.Converters
{
    public class StyleSelectorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 3 || !(values[0] is bool))
                return null;

            bool condition = (bool)values[0];
            return condition ? values[1] : values[2];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
