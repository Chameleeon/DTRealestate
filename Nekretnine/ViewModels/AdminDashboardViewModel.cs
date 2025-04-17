using Caliburn.Micro;
using Nekretnine.Events;
using Nekretnine.Services;

namespace Nekretnine.ViewModels
{
    internal class AdminDashboardViewModel : Screen
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IAgentService _agentService;

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

        public AdminDashboardViewModel(IAuthService authService, IUserService userService, IEventAggregator eventAggregator, IAgentService agentService)
        {
            _authService = authService;
            _userService = userService;
            _eventAggregator = eventAggregator;
            _agentService = agentService;
            CurrentSection = IoC.Get<AdminHomeViewModel>();
        }

        public void Agents()
        {
            CurrentSection = IoC.Get<AdminAgentsViewModel>();
        }

        public void Home()
        {
            CurrentSection = IoC.Get<AdminHomeViewModel>();
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
    }
}
