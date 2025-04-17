using Caliburn.Micro;
using Nekretnine.Events;
using Nekretnine.Services;

namespace Nekretnine.ViewModels
{
    public class MainViewModel : Conductor<object>, IHandle<ShowViewEvent>
    {
        private readonly IAuthService _authService;
        private readonly IEventAggregator _eventAggregator;

        public MainViewModel(IEventAggregator eventAggregator, IAuthService authService)
        {
            _authService = authService;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            _eventAggregator.PublishOnUIThreadAsync(new ShowViewEvent(typeof(LoginViewModel)));
        }

        public async Task HandleAsync(ShowViewEvent message, CancellationToken cancellationToken)
        {
            var viewModelType = message.ViewModelType;

            var viewModel = IoC.GetInstance(viewModelType, null);

            cancellationToken.ThrowIfCancellationRequested();

            await ActivateItemAsync(viewModel);
        }

    }
}
