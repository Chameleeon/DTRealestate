using System.Windows;

namespace Nekretnine.Converters
{
    public static class NavigationProperties
    {
        public static readonly DependencyProperty ViewModelTypeProperty =
            DependencyProperty.RegisterAttached("ViewModelType", typeof(Type), typeof(NavigationProperties), new PropertyMetadata(null));

        public static void SetViewModelType(DependencyObject obj, Type value)
        {
            obj.SetValue(ViewModelTypeProperty, value);
        }

        public static Type GetViewModelType(DependencyObject obj)
        {
            return (Type)obj.GetValue(ViewModelTypeProperty);
        }
    }
}