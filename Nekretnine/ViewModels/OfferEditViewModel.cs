using System.Collections.ObjectModel;
using System.Windows.Input;
using Caliburn.Micro;
using Microsoft.IdentityModel.Tokens;
using Nekretnine.Localization;
using Nekretnine.Models;
using Nekretnine.Services;

namespace Nekretnine.ViewModels
{
    public class OfferEditViewModel : Screen
    {
        private readonly IOfferService _offerService;
        private readonly IAuthService _authService;
        private readonly Offer _originalOffer;

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        private string _shortDescription;
        public string ShortDescription
        {
            get => _shortDescription;
            set
            {
                _shortDescription = value;
                NotifyOfPropertyChange(() => ShortDescription);
            }
        }

        private string _price;
        public string Price
        {
            get => _price;
            set
            {
                _price = value;
                NotifyOfPropertyChange(() => Price);
            }
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                _statusMessage = value;
                NotifyOfPropertyChange(() => StatusMessage);
            }
        }

        public ObservableCollection<Realestate> RealestateOptions { get; set; }
        public Realestate SelectedRealestate { get; set; }

        public ObservableCollection<Offertype> OfferTypeOptions { get; set; }
        private string _selectedOfferTypeDisplay;
        public string SelectedOfferTypeDisplay
        {
            get => _selectedOfferTypeDisplay;
            set
            {
                _selectedOfferTypeDisplay = value;
                NotifyOfPropertyChange(() => SelectedOfferTypeDisplay);
            }
        }

        private Offertype _selectedOfferType;
        public Offertype SelectedOfferType
        {
            get => _selectedOfferType;
            set
            {
                _selectedOfferType = value;
                SelectedOfferTypeDisplay = value?.OfferType1;
                NotifyOfPropertyChange(() => SelectedOfferType);
            }
        }

        public ICommand SaveOfferCommand { get; }
        public ICommand CancelCommand { get; }
        public OfferEditViewModel(IAuthService authService, IOfferService offerService, Offer offer, Realestate realestate)
        {
            _offerService = offerService;
            _authService = authService;
            _originalOffer = offer;

            SaveOfferCommand = new RelayCommand(_ => SaveOfferAsync());
            CancelCommand = new RelayCommand(_ => CancelAsync());

            RealestateOptions = new ObservableCollection<Realestate>();
            OfferTypeOptions = new ObservableCollection<Offertype>();
            SelectedRealestate = realestate;

            Title = offer.Title;
            ShortDescription = offer.ShortDescription;
            Price = offer.Price.ToString();
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            var realestates = await _offerService.GetRealestateOptionsAsync();
            var offerTypes = await _offerService.GetOfferTypeOptionsAsync();

            RealestateOptions.Clear();
            foreach (var realestate in realestates)
            {
                RealestateOptions.Add(realestate);
            }

            OfferTypeOptions.Clear();
            foreach (var offerType in offerTypes)
            {
                OfferTypeOptions.Add(offerType);

                if (offerType.IdType == _originalOffer.TipIdTip)
                {
                    SelectedOfferType = offerType;
                }
            }
        }

        private async Task SaveOfferAsync()
        {
            var localizer = Localizer.Instance;

            if (!Title.IsNullOrEmpty() &&
                !ShortDescription.IsNullOrEmpty() &&
                !Price.IsNullOrEmpty() &&
                SelectedOfferType != null)
            {
                if (!decimal.TryParse(Price, out var parsedPrice))
                {
                    StatusMessage = localizer["invalid_price"];
                    return;
                }

                try
                {
                    _originalOffer.Title = Title;
                    _originalOffer.ShortDescription = ShortDescription;
                    _originalOffer.Price = parsedPrice;
                    _originalOffer.TipIdTip = SelectedOfferType.IdType;

                    await _offerService.UpdateOfferAsync(_originalOffer);
                    StatusMessage = localizer["offer_update_success"];
                    await TryCloseAsync(true);
                }
                catch (Exception ex)
                {
                    StatusMessage = localizer["offer_update_error"];
                }
            }
            else
            {
                StatusMessage = localizer["mandatory_fields"];
            }
        }


        private async Task CancelAsync()
        {
            await TryCloseAsync(false);
        }
    }
}