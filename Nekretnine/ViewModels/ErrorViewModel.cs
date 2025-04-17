using Caliburn.Micro;

namespace Nekretnine.ViewModels
{
    public class ErrorViewModel : Screen
    {
        public string Message { get; }

        public ErrorViewModel(string message)
        {
            Message = message;
        }
    }
}
