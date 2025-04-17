using System.Windows;
using Nekretnine.Localization;
using Nekretnine.ViewModels;

namespace Nekretnine
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ApplyPersistedTheme();
            string lang = Nekretnine.Properties.Settings.Default.Language ?? "en";
            Localizer.Instance.SetCulture(lang);

            ApplyPersistedTheme();
        }

        private void ApplyPersistedTheme()
        {
            string savedTheme = Nekretnine.Properties.Settings.Default.AppTheme;

            if (!Enum.TryParse(savedTheme, out SettingsViewModel.ThemeType theme))
            {
                theme = SettingsViewModel.ThemeType.Flame;
            }

            var themeDictionary = new ResourceDictionary();
            string themePath = theme switch
            {
                SettingsViewModel.ThemeType.Dark => "pack://application:,,,/Nekretnine;component/Styles/Themes/Dark.xaml",
                SettingsViewModel.ThemeType.Light => "pack://application:,,,/Nekretnine;component/Styles/Themes/Light.xaml",
                SettingsViewModel.ThemeType.Flame => "pack://application:,,,/Nekretnine;component/Styles/Themes/Flame.xaml"
            };

            themeDictionary.Source = new Uri(themePath);

            var app = Application.Current;

            for (int i = app.Resources.MergedDictionaries.Count - 1; i >= 0; i--)
            {
                var dict = app.Resources.MergedDictionaries[i];
                if (dict.Source != null &&
                    (dict.Source.OriginalString.Contains("Themes/Dark.xaml") ||
                     dict.Source.OriginalString.Contains("Themes/Light.xaml") ||
                     dict.Source.OriginalString.Contains("Themes/Flame.xaml")))
                {
                    app.Resources.MergedDictionaries.RemoveAt(i);
                }
            }

            app.Resources.MergedDictionaries.Add(themeDictionary);
        }

    }
}
