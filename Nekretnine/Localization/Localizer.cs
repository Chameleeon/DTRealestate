using System.ComponentModel;
using System.Globalization;
using System.Resources;
using Nekretnine.Properties;

namespace Nekretnine.Localization
{
    public class Localizer : INotifyPropertyChanged
    {
        public static Localizer Instance { get; } = new();

        private CultureInfo _currentCulture = CultureInfo.CurrentUICulture;
        private readonly ResourceManager _resourceManager = Resources.ResourceManager;

        public event PropertyChangedEventHandler PropertyChanged;

        public string this[string key] => _resourceManager.GetString(key, _currentCulture);

        public void SetCulture(string cultureCode)
        {
            _currentCulture = new CultureInfo(cultureCode);
            CultureInfo.DefaultThreadCurrentUICulture = _currentCulture;
            CultureInfo.DefaultThreadCurrentCulture = _currentCulture;
            OnPropertyChanged(string.Empty);
        }

        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}