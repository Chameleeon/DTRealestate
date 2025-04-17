using System.Globalization;
using System.Windows.Data;
using Nekretnine.Localization;

namespace Nekretnine.Converters
{
    public class BoolToStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isActive = (bool)value;
            var localizer = Localizer.Instance;
            return isActive ? localizer["activated"] : localizer["deactivated"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
