using System.Globalization;
using System.Windows.Data;
using Nekretnine.Localization;

namespace Nekretnine.Converters
{
    public class BoolToButtonContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isActive = (bool)value;

            var localizer = Localizer.Instance;
            return isActive ? localizer["deactivate_button"] : localizer["activate_button"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
