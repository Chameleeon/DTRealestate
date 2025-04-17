using System.Collections.ObjectModel;
using System.Windows.Input;
using Caliburn.Micro;
using Microsoft.IdentityModel.Tokens;
using Nekretnine.Models;
using Nekretnine.Services;

namespace Nekretnine.ViewModels
{
    public class OfferAddViewModel : Screen
    {
        private readonly IOfferService _offerService;
        private readonly IAuthService _authSercvice;
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

        public OfferAddViewModel(IAuthService authService, IOfferService offerService, Realestate realestate)
        {
            SaveOfferCommand = new RelayCommand(_ => SaveOffer());
            CancelCommand = new RelayCommand(_ => Cancel());
            _offerService = offerService;
            _authSercvice = authService;

            RealestateOptions = new ObservableCollection<Realestate>();
            OfferTypeOptions = new ObservableCollection<Offertype>();
            SelectedRealestate = realestate;
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
            }
        }

        private async Task SaveOffer()
        {
            if (!Title.IsNullOrEmpty() && !ShortDescription.IsNullOrEmpty() && !Price.IsNullOrEmpty() && SelectedOfferType != null)
            {
                Offer newOffer = new Offer
                {
                    Title = Title,
                    ShortDescription = ShortDescription,
                    Price = decimal.Parse(Price),
                    TipIdTip = SelectedOfferType.IdType,
                    RealestateIdRealestate = SelectedRealestate.IdRealestate,
                    AgentKorisnikIdKorisnik = _authSercvice.LogedInUser.IdUser,
                    Active = 1
                };

                try
                {
                    await _offerService.SaveOfferAsync(newOffer);
                    StatusMessage = "Offer saved successfully";
                    await TryCloseAsync(true);
                }
                catch (Exception ex)
                {
                    StatusMessage = $"Error saving offer: {ex.Message} + {ex.InnerException.Message}";
                }
            }
            else
            {
                StatusMessage = "All fields are mandatory";
            }
        }


        private void Cancel()
        {
            TryCloseAsync(false);
        }
    }
}
