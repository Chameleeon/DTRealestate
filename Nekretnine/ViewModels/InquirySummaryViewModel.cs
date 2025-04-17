using Caliburn.Micro;
using Nekretnine.Events;
using Nekretnine.Models;
using Nekretnine.Services;

namespace Nekretnine.ViewModels
{
    public class InquirySummaryViewModel : PropertyChangedBase
    {
        private readonly IInquiryService _inquiryService;
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        private string _summary;
        public string Summary
        {
            get => _summary;
            set
            {
                _summary = value;
                NotifyOfPropertyChange(() => Summary);
            }
        }

        public Inquiry FullInquiry { get; set; }

        public InquirySummaryViewModel(
            IInquiryService inquiryService,
            IWindowManager windowManager,
            IEventAggregator eventAggregator)
        {
            _inquiryService = inquiryService ?? throw new ArgumentNullException(nameof(inquiryService));
            _windowManager = windowManager ?? throw new ArgumentNullException(nameof(windowManager));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
        }

        public async Task ShowInfo()
        {
            try
            {
                string searchQuery = FullInquiry.OfferIdOfferNavigation.Title;

                await _eventAggregator.PublishOnUIThreadAsync(new NavigateToOffersEvent(searchQuery));
            }
            catch (Exception ex)
            {
                await _windowManager.ShowDialogAsync(new ErrorViewModel($"Error opening offers view: {ex.Message}"));
            }
        }


        public async Task Delete(bool popup = true)
        {
            try
            {
                bool? result = true;
                if (popup)
                {
                    result = await _windowManager.ShowDialogAsync(
                        new ConfirmationViewModel($"Are you sure you want to delete the inquiry '{Title}'?"));
                }


                if (result != true) return;

                await _inquiryService.DeleteInquiryAsync(FullInquiry.IdInquiry);
                _eventAggregator.PublishOnUIThreadAsync(new NavigateToInquiriesEvent());

            }
            catch (Exception ex)
            {
                await _windowManager.ShowDialogAsync(new ErrorViewModel($"Error deleting inquiry: {ex.Message}"));
            }
        }

        public async Task Reply()
        {
            try
            {
                var replyViewModel = new ReplyViewModel(FullInquiry, _inquiryService, _eventAggregator, this);
                await _windowManager.ShowDialogAsync(replyViewModel);
            }
            catch (Exception ex)
            {
                await _windowManager.ShowDialogAsync(new ErrorViewModel($"Error opening reply view: {ex.Message}"));
            }
        }
    }
}