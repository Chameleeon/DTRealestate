using System.Collections.ObjectModel;
using System.IO;
using Caliburn.Micro;
using Nekretnine.Models;
using Nekretnine.Services;
using Newtonsoft.Json;

namespace Nekretnine.ViewModels
{
    public class RealestatesCardViewModel : Screen
    {
        private string _title;
        private ObservableCollection<string> _imagePaths = new ObservableCollection<string>();
        private Realestate _realestate;
        public Realestate Realestate { get { return _realestate; } }
        private int _currentImageIndex;
        private System.Timers.Timer _slideshowTimer;
        private readonly IOfferService _offerService;
        private readonly IWindowManager _windowManager;
        private readonly IAuthService _authService;

        public RealestatesCardViewModel(IAuthService authService, IOfferService offerService, Realestate realestate, IWindowManager windowManager)
        {
            _slideshowTimer = new System.Timers.Timer(3000);
            _slideshowTimer.Elapsed += (s, e) => NextImage();
            _slideshowTimer.Start();
            _offerService = offerService;
            _windowManager = windowManager;
            _realestate = realestate;
            _authService = authService;

            Address adr = realestate.AdresaIdAdresaNavigation;
            Title = realestate.IdRealestate + " - " + adr.Number + " " + adr.Street + ", " + adr.City;

            var imagePathsJson = realestate.ImagePaths;
            var imagePaths = JsonConvert.DeserializeObject<List<ImagePath>>(imagePathsJson);

            foreach (ImagePath i in imagePaths)
            {
                ImagePaths.Add(i.Path);
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        public ObservableCollection<string> ImagePaths
        {
            get => _imagePaths;
            set
            {
                _imagePaths = value;
                NotifyOfPropertyChange(() => ImagePaths);
            }
        }
        public string ImageSource
        {
            get { return GetImageSource(); }
        }

        public string CurrentImagePath => _imagePaths.Count > 0 ? _imagePaths[_currentImageIndex] : string.Empty;

        public void NextImage()
        {
            if (ImagePaths.Count == 0) return;
            _currentImageIndex = (_currentImageIndex + 1) % ImagePaths.Count;
            NotifyOfPropertyChange(() => CurrentImagePath);
            NotifyOfPropertyChange(() => ImageSource);
        }

        public void PreviousImage()
        {
            if (ImagePaths.Count == 0) return;
            _currentImageIndex = (_currentImageIndex - 1 + ImagePaths.Count) % ImagePaths.Count;
            NotifyOfPropertyChange(() => CurrentImagePath);
            NotifyOfPropertyChange(() => ImageSource);
        }

        private string GetImageSource()
        {
            string imagePath = Path.Combine(Environment.CurrentDirectory, CurrentImagePath);

            if (File.Exists(imagePath))
            {
                return imagePath;
            }

            return string.Empty;
        }

        public async void AddOffer()
        {
            var offerDetailsVM = new OfferAddViewModel(_authService, _offerService, _realestate);
            bool? result = await _windowManager.ShowDialogAsync(offerDetailsVM);
        }

        public bool MatchesQuery(string query)
        {
            query = query.ToLowerInvariant();
            return Title.ToLowerInvariant().Contains(query)
                || _realestate.Description.ToLowerInvariant().Contains(query) || _realestate.AdresaIdAdresaNavigation.City.Contains(query) || _realestate.AdresaIdAdresaNavigation.Street.Contains(query) || _realestate.AdresaIdAdresaNavigation.Number.Contains(query);
        }

        public async void Edit(Realestate r)
        {
            var editOfferVM = new RealestateEditViewModel(IoC.Get<IEventAggregator>(), IoC.Get<IRealestateService>(), Realestate);
            await _windowManager.ShowDialogAsync(editOfferVM);
        }

        public async void ShowInfo()
        {
            var detailsVM = new RealestateDetailsViewModel(Realestate);
            await _windowManager.ShowDialogAsync(detailsVM);
        }
    }
}
