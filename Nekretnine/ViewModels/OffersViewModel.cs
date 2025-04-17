using System.Collections.ObjectModel;
using Caliburn.Micro;
using Nekretnine.Localization;

namespace Nekretnine.ViewModels
{
    public class OffersViewModel : Screen
    {
        private readonly IOfferService _offerService;
        private readonly IWindowManager _windowManager;


        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                NotifyOfPropertyChange(() => SearchQuery);
            }
        }

        private string _selectedOfferType;
        public string SelectedOfferType
        {
            get => _selectedOfferType;
            set
            {
                if (_selectedOfferType != value)
                {
                    _selectedOfferType = value;
                    NotifyOfPropertyChange(() => SelectedOfferType);
                    FilterOffers();
                }
            }
        }

        public ObservableCollection<string> OfferTypes { get; } = new()
        {
            Localizer.Instance["all_selection"],
            Localizer.Instance["for_sale"],
            Localizer.Instance["for_rent"]
        };



        public ObservableCollection<OfferCardViewModel> Offers { get; set; } = new();
        private List<OfferCardViewModel> _allOffers = new();


        public OffersViewModel(IOfferService offerService, IWindowManager windowManager)
        {
            _offerService = offerService;
            LoadOffers();
            var localizer = Localizer.Instance;
            _selectedOfferType = localizer["all_selection"];
            _windowManager = windowManager;

        }

        private async void LoadOffers()
        {
            var offersFromDb = await _offerService.GetAllOffersAsync();
            _allOffers = offersFromDb.Select(o => new OfferCardViewModel(o)).ToList();
            Offers.Clear();
            foreach (var offer in _allOffers)
            {
                Offers.Add(offer);
            }
            Search();
        }

        public void Search()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                Offers.Clear();
                foreach (var offer in _allOffers)
                    Offers.Add(offer);
            }
            else
            {
                var filtered = _allOffers
                    .Where(o => o.MatchesQuery(SearchQuery))
                    .ToList();

                Offers.Clear();
                foreach (var offer in filtered)
                    Offers.Add(offer);
            }
        }

        public void ClearSearch()
        {
            SearchQuery = string.Empty;
            Search();
        }

        private void FilterOffers()
        {
            var filtered = _allOffers.AsEnumerable();
            var localizer = Localizer.Instance;
            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                var query = SearchQuery.ToLowerInvariant();
                filtered = filtered.Where(o => o.MatchesQuery(query));
            }

            if (SelectedOfferType == localizer["for_sale"])
                filtered = filtered.Where(o => o.Offer.TipIdTipNavigation.OfferType1 == "Sale");
            else if (SelectedOfferType == localizer["for_rent"])
                filtered = filtered.Where(o => o.Offer.TipIdTipNavigation.OfferType1 == "Rent");

            Offers.Clear();
            foreach (var offer in filtered)
                Offers.Add(offer);
        }

    }
}