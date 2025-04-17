using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Media;
using Caliburn.Micro;
using Nekretnine.Events;
using Nekretnine.Helpers;
using Nekretnine.Localization;
using Nekretnine.Models;
using Nekretnine.Services;

namespace Nekretnine.ViewModels
{
    class SignupViewModel : Screen, INotifyPropertyChanged
    {
        private readonly IAuthService _authService;
        private readonly IEventAggregator _eventAggregator;
        private string _username = "";

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                NotifyOfPropertyChange(() => Username);
            }
        }

        private string _passsword = "";

        public string Password
        {
            get { return _passsword; }
            set { _passsword = value; }
        }

        private string _confirmPassword = "";

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; }
        }

        private string _firstName = "";

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private string _lastName = "";

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
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

        public SignupViewModel(IEventAggregator eventAggregator, IAuthService authService)
        {
            _authService = authService;
            _eventAggregator = eventAggregator;
        }

        public async Task Signup()
        {
            var localizer = Localizer.Instance;
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(Email))
            {
                StatusMessage = localizer["mandatory_fields"];
                StatusColor = Brushes.Red;
                return;
            }

            if (!Regex.IsMatch(Username, @"^[a-zA-Z0-9]+$"))
            {
                StatusMessage = localizer["error_username"];
                StatusColor = Brushes.Red;
                return;
            }

            if (Password.Length < 8)
            {
                StatusMessage = localizer["error_password"];
                StatusColor = Brushes.Red;
                return;
            }

            if (Password != ConfirmPassword)
            {
                StatusMessage = localizer["error_confirm_pass"];
                StatusColor = Brushes.Red;
                return;
            }

            if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                StatusMessage = localizer["error_email"];
                StatusColor = Brushes.Red;
                return;
            }

            if (!Regex.IsMatch(PhoneNumber, @"^\+?\d{7,15}$"))
            {
                StatusMessage = localizer["error_phone"];
                StatusColor = Brushes.Red;
                return;
            }

            var (hash, salt) = PasswordHasher.HashPassword(Password);

            var user = new User
            {
                Username = Username,
                Password = hash,
                Salt = salt,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email
            };

            string message = await _authService.RegisterUserAsync(user);

            var newAgent = new Agent
            {
                KorisnikIdKorisnik = user.IdUser,
                KorisnikIdKorisnikNavigation = user,
                PhoneNumber = PhoneNumber
            };
            if (message.Contains("success", StringComparison.OrdinalIgnoreCase) || message.Contains("uspje", StringComparison.OrdinalIgnoreCase))
            {
                await _authService.RegisterAgentAsync(newAgent);
            }


            StatusMessage = message;
            StatusColor = (message.Contains("success", StringComparison.OrdinalIgnoreCase) || message.Contains("uspje", StringComparison.OrdinalIgnoreCase)) ? Brushes.Green : Brushes.Red;
        }



        public async void OpenLogIn()
        {
            await _eventAggregator.PublishOnUIThreadAsync(new ShowViewEvent(typeof(LoginViewModel)));
        }
    }
}
