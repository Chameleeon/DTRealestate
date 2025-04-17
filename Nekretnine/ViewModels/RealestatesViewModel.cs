using System.Collections.ObjectModel;
using Caliburn.Micro;
using Nekretnine.Localization;
using Nekretnine.Services;
namespace Nekretnine.ViewModels
{
    public class RealestatesViewModel : Screen
    {
        private readonly IRealestateService _realestateService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;
        private readonly IOfferService _offerService;
        private readonly IAuthService _authService;

        public List<string> FilterOptions { get; }
        public List<string> SortOptions { get; }

        public ObservableCollection<RealestatesCardViewModel> Realestates { get; set; } = new();
        private List<RealestatesCardViewModel> _allRealestates = new();

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

        private string _selectedFilter;
        public string SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                NotifyOfPropertyChange(() => SelectedFilter);
                ApplyFilters();
            }
        }

        private string _selectedSort;
        public string SelectedSort
        {
            get => _selectedSort;
            set
            {
                _selectedSort = value;
                NotifyOfPropertyChange(() => SelectedSort);
                ApplyFilters();
            }
        }


        public RealestatesViewModel(IEventAggregator eventAggregator, IAuthService authService, IRealestateService realestateService, IWindowManager windowManager, IOfferService offerService)
        {
            _eventAggregator = eventAggregator;
            _realestateService = realestateService;
            _windowManager = windowManager;
            _offerService = offerService;
            _authService = authService;
            LoadRealestates();
            var localizer = Localizer.Instance;
            _selectedSort = localizer["newest"];
            _selectedFilter = localizer["all_selection"];
            SortOptions = new() { localizer["newest"], localizer["oldest"] };
            FilterOptions = new() { localizer["all_selection"], localizer["has_offer"], localizer["no_offer"] };
        }

        private async void LoadRealestates()
        {
            var realestatesFromDb = await _realestateService.GetAllRealestatesAsync();
            Realestates.Clear();
            _allRealestates = realestatesFromDb.Select(r => new RealestatesCardViewModel(_authService, _offerService, r, _windowManager)).ToList();
            foreach (var realestate in _allRealestates)
            {
                Realestates.Add(realestate);
            }
        }

        public async void AddRealestate()
        {
            var realestateDetailsVM = new RealestateAddViewModel(_realestateService);
            bool? result = await _windowManager.ShowDialogAsync(realestateDetailsVM);
            if (result == true)
            {
                LoadRealestates();
            }
        }

        public void Search()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                Realestates.Clear();
                foreach (var offer in _allRealestates)
                    Realestates.Add(offer);
            }
            else
            {
                var filtered = _allRealestates
                    .Where(r => r.MatchesQuery(SearchQuery))
                .ToList();

                Realestates.Clear();
                foreach (var offer in filtered)
                    Realestates.Add(offer);
            }

            ApplyFilters();
        }

        public void ClearSearch()
        {
            SearchQuery = string.Empty;
            Search();
        }

        private void ApplyFilters()
        {
            var filtered = _allRealestates.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                filtered = filtered.Where(r => r.MatchesQuery(SearchQuery));
            }

            switch (SelectedFilter)
            {
                case "Has Offer":
                    filtered = filtered.Where(r => r.Realestate.Offers?.Any() == true);
                    break;
                case "Ima ponude":
                    filtered = filtered.Where(r => r.Realestate.Offers?.Any() == true);
                    break;
                case "No Offer":
                    filtered = filtered.Where(r => r.Realestate.Offers?.Any() != true);
                    break;
                case "Nema ponude":
                    filtered = filtered.Where(r => r.Realestate.Offers?.Any() != true);
                    break;
                case "All":
                default:
                    break;
            }

            switch (SelectedSort)
            {
                case "Newest":
                    filtered = filtered.OrderByDescending(r => r.Realestate.DateAdded);
                    break;
                case "Najnovije":
                    filtered = filtered.OrderByDescending(r => r.Realestate.DateAdded);
                    break;
                case "Oldest":
                    filtered = filtered.OrderBy(r => r.Realestate.DateAdded);
                    break;
                case "Najstarije":
                    filtered = filtered.OrderBy(r => r.Realestate.DateAdded);
                    break;
            }

            Realestates.Clear();
            foreach (var r in filtered)
            {
                Realestates.Add(r);
            }
        }
    }
}