using System.ComponentModel;
using System.Windows.Media;
using Caliburn.Micro;
using Nekretnine.Events;
using Nekretnine.Localization;
using Nekretnine.Models;
using Nekretnine.Services;

namespace Nekretnine.ViewModels
{
    internal class LoginViewModel : Screen, INotifyPropertyChanged
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IEventAggregator _eventAggregator;
        private string _username = "";

        public LoginViewModel(IEventAggregator eventAggregator, IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
            _eventAggregator = eventAggregator;
        }
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _passsword = "";

        public string Password
        {
            get { return _passsword; }
            set { _passsword = value; }
        }


        private string _statusMessage = "";
        public string StatusMessage
        {
            get => _statusMessage;
            set => Set(ref _statusMessage, value);
        }

        private Brush _statusColor = Brushes.Red;
        public Brush StatusColor
        {
            get => _statusColor;
            set => Set(ref _statusColor, value);
        }

        public async Task Login()
        {
            var result = await _authService.LoginAsync(Username, Password);
            var localizer = Localizer.Instance;

            if (result)
            {
                User user = await _userService.GetUserByUsername(Username);
                if (user.Administrator != null)
                {
                    _authService.LogedInUser = user;
                    await _eventAggregator.PublishOnUIThreadAsync(new ShowViewEvent(typeof(AdminDashboardViewModel)));
                }
                {

                }
                if (user.Agent != null)
                {
                    if (!user.Agent.Activated)
                    {
                        StatusMessage = localizer["wait_for_activation"];
                        StatusColor = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        _authService.LogedInUser = user;
                        await _eventAggregator.PublishOnUIThreadAsync(new ShowViewEvent(typeof(DashboardViewModel)));
                    }
                }
            }

            else
            {
                StatusMessage = localizer["invalid_user_or_pass"];
                StatusColor = new SolidColorBrush(Colors.Red);
            }
        }

        public async void OpenSignUp()
        {
            await _eventAggregator.PublishOnUIThreadAsync(new ShowViewEvent(typeof(SignupViewModel)));
        }
    }
}
