using Caliburn.Micro;

namespace Nekretnine.ViewModels
{
    public class ConfirmationViewModel : Screen
    {
        private string _message;
        private string _title;
        private bool? _dialogResult;

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        public bool? DialogResult
        {
            get => _dialogResult;
            set
            {
                _dialogResult = value;
                NotifyOfPropertyChange(() => DialogResult);
            }
        }

        public ConfirmationViewModel(string message, string title = "Confirmation")
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Title = title;
        }

        public void Confirm()
        {
            DialogResult = true;
            TryCloseAsync(true);
        }

        public void Cancel()
        {
            DialogResult = false;
            TryCloseAsync(false);
        }
    }
}