using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Nekretnine.Converters
{
    public class ButtonStyleConverter : IValueConverter
    {
        public Type TargetViewModel { get; set; }
        public Style ActiveStyle { get; set; }
        public Style InactiveStyle { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || TargetViewModel == null)
                return InactiveStyle;

            return value.GetType() == TargetViewModel ? ActiveStyle : InactiveStyle;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
