using System.Windows;
using Caliburn.Micro;
using Nekretnine.Localization;

namespace Nekretnine.ViewModels
{
    public class SettingsViewModel : Screen
    {
        public enum ThemeType
        {
            Dark,
            Light,
            Flame
        }
        private string _selectedLanguage;

        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                _selectedLanguage = value;
                NotifyOfPropertyChange(() => SelectedLanguage);
            }
        }

        private ThemeType _currentTheme;
        private string _currentThemeName;

        public SettingsViewModel()
        {
            LoadCurrentTheme();
            SelectedLanguage = Properties.Settings.Default.Language ?? "en";
        }

        public string CurrentThemeName
        {
            get { return _currentThemeName; }
            set
            {
                _currentThemeName = value;
                NotifyOfPropertyChange(() => CurrentThemeName);
            }
        }

        private void LoadCurrentTheme()
        {
            try
            {
                var savedTheme = Properties.Settings.Default.AppTheme;
                _currentTheme = (ThemeType)Enum.Parse(typeof(ThemeType), savedTheme);
            }
            catch
            {
                _currentTheme = ThemeType.Dark;
            }

            CurrentThemeName = _currentTheme.ToString();
        }

        public void SelectDarkTheme()
        {
            ApplyTheme(ThemeType.Dark);
        }

        public void SelectLightTheme()
        {
            ApplyTheme(ThemeType.Light);
        }

        public void SelectFlameTheme()
        {
            ApplyTheme(ThemeType.Flame);
        }

        private void ApplyTheme(ThemeType theme)
        {
            _currentTheme = theme;
            CurrentThemeName = theme.ToString();

            SaveThemeSetting(theme);

            ApplyThemeToApplication(theme);
        }

        private void SaveThemeSetting(ThemeType theme)
        {
            Properties.Settings.Default.AppTheme = theme.ToString();
            Properties.Settings.Default.Save();

        }

        private void ApplyThemeToApplication(ThemeType theme)
        {
            ResourceDictionary themeDictionary = new ResourceDictionary();

            Application app = Application.Current;

            for (int i = app.Resources.MergedDictionaries.Count - 1; i >= 0; i--)
            {
                var dictionary = app.Resources.MergedDictionaries[i];
                if (dictionary.Source != null &&
                    (dictionary.Source.OriginalString.Contains("Themes/Dark.xaml") ||
                     dictionary.Source.OriginalString.Contains("Themes/Light.xaml") ||
                     dictionary.Source.OriginalString.Contains("Themes/Flame.xaml")))
                {
                    app.Resources.MergedDictionaries.RemoveAt(i);
                }
            }

            string themePath = string.Empty;

            switch (theme)
            {
                case ThemeType.Dark:
                    themePath = "pack://application:,,,/Nekretnine;component/Styles/Themes/Dark.xaml";
                    break;
                case ThemeType.Light:
                    themePath = "pack://application:,,,/Nekretnine;component/Styles/Themes/Light.xaml";
                    break;
                case ThemeType.Flame:
                    themePath = "pack://application:,,,/Nekretnine;component/Styles/Themes/Flame.xaml";
                    break;
            }

            if (!string.IsNullOrEmpty(themePath))
            {
                themeDictionary.Source = new Uri(themePath);
                app.Resources.MergedDictionaries.Add(themeDictionary);
            }
        }

        public void SetEnglish()
        {
            ChangeLanguage("en");
        }

        public void SetSerbian()
        {
            ChangeLanguage("sr");
        }

        private void ChangeLanguage(string cultureCode)
        {
            Localizer.Instance.SetCulture(cultureCode);
            Properties.Settings.Default.Language = cultureCode;
            Properties.Settings.Default.Save();
            SelectedLanguage = cultureCode;
        }

    }
}