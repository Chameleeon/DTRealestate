using System.Collections.ObjectModel;
using System.IO;
using Caliburn.Micro;
using Nekretnine.Models;
using Nekretnine.Services;
using Newtonsoft.Json;

namespace Nekretnine.ViewModels
{
    public class OfferCardViewModel : PropertyChangedBase
    {
        public Offer Offer { get; set; }
        private int _currentImageIndex;
        private System.Timers.Timer _slideshowTimer;
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        private string _price;
        public string Price
        {
            get { return _price; }
            set
            {
                _price = value;
                NotifyOfPropertyChange(() => Price);
            }
        }

        private string _shortDescription;
        public string ShortDescription
        {
            get { return _shortDescription; }
            set
            {
                _shortDescription = value;
                NotifyOfPropertyChange(() => ShortDescription);
            }
        }

        private ObservableCollection<string> _imagePaths = new ObservableCollection<string>();
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

        public OfferCardViewModel(Offer offer)
        {
            Title = offer.Title;
            Price = offer.Price.ToString("C");
            ShortDescription = offer.ShortDescription;
            Offer = offer;

            _slideshowTimer = new System.Timers.Timer(3000);
            _slideshowTimer.Elapsed += (s, e) => NextImage();
            _slideshowTimer.Start();

            var imagePathsJson = offer.RealestateIdRealestateNavigation.ImagePaths;
            var imagePaths = JsonConvert.DeserializeObject<List<ImagePath>>(imagePathsJson);

            foreach (ImagePath i in imagePaths)
            {
                ImagePaths.Add(i.Path);
            }
        }

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
        public bool MatchesQuery(string query)
        {
            query = query.ToLowerInvariant();
            return Title.ToLowerInvariant().Contains(query)
                || ShortDescription.ToLowerInvariant().Contains(query);
        }

        public async void Edit()
        {
            var windowManager = IoC.Get<IWindowManager>();
            var eventAggregator = IoC.Get<IEventAggregator>();
            var authService = IoC.Get<IAuthService>();
            var offerService = IoC.Get<IOfferService>();

            var editViewModel = new OfferEditViewModel(authService, offerService, Offer, Offer.RealestateIdRealestateNavigation);

            bool? result = await windowManager.ShowDialogAsync(editViewModel);

            if (result == true)
            {
                Title = Offer.Title;
                Price = Offer.Price.ToString("C");
                ShortDescription = Offer.ShortDescription;


                ImagePaths.Clear();
                var imagePaths = JsonConvert.DeserializeObject<List<ImagePath>>(
                    Offer.RealestateIdRealestateNavigation.ImagePaths);

                foreach (var imagePath in imagePaths)
                {
                    ImagePaths.Add(imagePath.Path);
                }

                _currentImageIndex = 0;
                NotifyOfPropertyChange(() => ImageSource);
            }
        }
        public async void AddContract()
        {
            var windowManager = IoC.Get<IWindowManager>();
            var contractService = IoC.Get<IContractService>();
            var userService = IoC.Get<IUserService>();
            var contractAddVM = new ContractAddViewModel(windowManager, contractService, userService, Offer);
            bool? result = await windowManager.ShowDialogAsync(contractAddVM);
        }

        public async void ShowDetails()
        {
            var detailsVM = new OfferDetailsViewModel(Offer);
            await IoC.Get<IWindowManager>().ShowDialogAsync(detailsVM);
        }
    }
}
