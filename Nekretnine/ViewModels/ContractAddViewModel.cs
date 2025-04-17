using System.Collections.ObjectModel;
using Caliburn.Micro;
using Nekretnine.Localization;
using Nekretnine.Models;
using Nekretnine.Services;

namespace Nekretnine.ViewModels
{
    public class ContractAddViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private readonly IContractService _contractService;
        private readonly IUserService _userService;

        public ContractAddViewModel(IWindowManager windowManager, IContractService contractService, IUserService userService, Offer offer)
        {
            _windowManager = windowManager;
            _userService = userService;
            _contractService = contractService;
            Offer = offer;

            LoadDataAsync();

            ClientOptions = new ObservableCollection<Client>();
        }
        new


        private async Task LoadDataAsync()
        {
            var clients = await _userService.GetClientsAsync();

            ClientOptions.Clear();
            foreach (var client in clients)
                ClientOptions.Add(client);
        }

        private Offer _offer;

        public Offer Offer
        {
            get { return _offer; }
            set { _offer = value; }
        }


        private string _content;
        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                NotifyOfPropertyChange(() => Content);
            }
        }

        private DateTime? _signDate = DateTime.Today;
        public DateTime? SignDate
        {
            get => _signDate;
            set
            {
                _signDate = value;
                NotifyOfPropertyChange(() => SignDate);
            }
        }

        private int _periodVazenja;
        public int PeriodVazenja
        {
            get => _periodVazenja;
            set
            {
                _periodVazenja = value;
                NotifyOfPropertyChange(() => PeriodVazenja);
            }
        }

        public ObservableCollection<Client> ClientOptions { get; }
        private Client? _selectedClient;
        public Client? SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                NotifyOfPropertyChange(() => SelectedClient);
            }
        }

        private string? _statusMessage;
        public string? StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                NotifyOfPropertyChange(() => StatusMessage);
            }
        }


        public async void SaveContract()
        {
            StatusMessage = null;

            if (!ValidateInput())
                return;

            try
            {
                var contract = new Contract
                {
                    Content = Content,
                    SignDate = DateOnly.FromDateTime(SignDate!.Value),
                    PeriodVazenja = PeriodVazenja,
                    OfferIdOffer = Offer.IdOffer,
                    ClientKorisnikIdKorisnik = SelectedClient!.KorisnikIdKorisnik
                };

                await _contractService.CreateContractAsync(contract);
                await TryCloseAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = Localizer.Instance["error_contract"];
            }
        }

        public void Cancel()
        {
            TryCloseAsync();
        }

        private bool ValidateInput()
        {
            var localizer = Localizer.Instance;
            if (string.IsNullOrWhiteSpace(Content))
            {
                StatusMessage = localizer["error_content_empty"];
                return false;
            }

            if (SignDate == null)
            {
                StatusMessage = localizer["error_signed_date"];
                return false;
            }

            if (PeriodVazenja <= 0)
            {
                StatusMessage = localizer["error_valid_period"];
                return false;
            }

            if (SelectedClient == null)
            {
                StatusMessage = localizer["error_select_client"];
                return false;
            }

            return true;
        }
    }
}
