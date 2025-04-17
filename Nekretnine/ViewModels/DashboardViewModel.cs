using Caliburn.Micro;
using Nekretnine.Events;
using Nekretnine.Models;
using Nekretnine.Services;

namespace Nekretnine.ViewModels
{
    public class DashboardViewModel : Conductor<object>, IHandle<NavigateToOffersEvent>, IHandle<NavigateToInquiriesEvent>, IHandle<RealestateUpdatedEvent>
    {
        public bool IsCurrentSection<T>() where T : class
        {
            return CurrentSection is T;
        }


        private object _currentSection;

        public object CurrentSection
        {
            get { return _currentSection; }
            set
            {
                if (_currentSection != value)
                {
                    _currentSection = value;
                    NotifyOfPropertyChange(() => CurrentSection);
                }
            }
        }

        private string _username;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IOfferService _offerService;
        private readonly IAgentService _agentService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRealestateService _realestateService;

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private User _curUser;

        public User CurUser
        {
            get { return _curUser; }
            set { _curUser = value; }
        }


        public DashboardViewModel(IUserService userService, IAuthService authService, IOfferService offerService, IEventAggregator eventAggregator, IRealestateService realestateService)
        {
            _userService = userService;
            _authService = authService;
            _curUser = _authService.LogedInUser;
            Username = _curUser.Username;
            CurrentSection = IoC.Get<HomeViewModel>();
            _offerService = offerService;
            _eventAggregator = eventAggregator;
            _realestateService = realestateService;
            _eventAggregator.Subscribe(this);
        }

        public void Home()
        {
            CurrentSection = IoC.Get<HomeViewModel>();
        }

        public void Realestates()
        {
            CurrentSection = IoC.Get<RealestatesViewModel>();
        }

        public void Contracts()
        {
            CurrentSection = IoC.Get<ContractsViewModel>();
        }

        public void Inquiries()
        {
            CurrentSection = IoC.Get<InquiriesViewModel>();
        }

        public void Offers()
        {
            CurrentSection = IoC.Get<OffersViewModel>();
        }

        public void Settings()
        {
            CurrentSection = IoC.Get<SettingsViewModel>();
        }

        public async Task Logout()
        {
            _authService.LogedInUser = null;
            await _eventAggregator.PublishOnUIThreadAsync(new ShowViewEvent(typeof(LoginViewModel)));
        }
        async Task IHandle<NavigateToOffersEvent>.HandleAsync(NavigateToOffersEvent message, CancellationToken cancellationToken)
        {
            var offersVm = IoC.Get<OffersViewModel>();
            offersVm.SearchQuery = message.SearchQuery;
            CurrentSection = offersVm;
        }

        async Task IHandle<NavigateToInquiriesEvent>.HandleAsync(NavigateToInquiriesEvent message, CancellationToken cancellationToken)
        {
            var inquiriesVm = IoC.Get<InquiriesViewModel>();
            CurrentSection = inquiriesVm;
        }

        async Task IHandle<RealestateUpdatedEvent>.HandleAsync(RealestateUpdatedEvent message, CancellationToken cancellationToken)
        {
            var realestateVm = IoC.Get<RealestatesViewModel>();
            CurrentSection = realestateVm;
        }
    }
}
