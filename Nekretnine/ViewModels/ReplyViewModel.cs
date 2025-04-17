using Caliburn.Micro;
using Nekretnine.Models;
using Nekretnine.Services;

namespace Nekretnine.ViewModels
{
    public class ReplyViewModel : Screen
    {
        private readonly IInquiryService _inquiryService;
        private readonly IEventAggregator _eventAggregator;

        private Inquiry _inquiry;
        private string _subject;
        private string _message;
        private bool _isSending;

        private InquirySummaryViewModel _inquiryViewModel;

        public Inquiry FullInquiry
        {
            get => _inquiry;
            set
            {
                _inquiry = value;
                NotifyOfPropertyChange(() => FullInquiry);
            }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; NotifyOfPropertyChange(() => Title); }
        }

        private string _summary;

        public string Summary
        {
            get { return _summary; }
            set { _summary = value; NotifyOfPropertyChange(() => Summary); }
        }



        public string Subject
        {
            get => _subject;
            set
            {
                _subject = value;
                NotifyOfPropertyChange(() => Subject);
                NotifyOfPropertyChange(() => CanSend);
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                NotifyOfPropertyChange(() => Message);
                NotifyOfPropertyChange(() => CanSend);
            }
        }

        public bool IsSending
        {
            get => _isSending;
            set
            {
                _isSending = value;
                NotifyOfPropertyChange(() => IsSending);
                NotifyOfPropertyChange(() => CanSend);
            }
        }

        public bool CanSend => !string.IsNullOrWhiteSpace(Subject) &&
                               !string.IsNullOrWhiteSpace(Message) &&
                               !IsSending;

        public ReplyViewModel(Inquiry inquiry,
                             IInquiryService inquiryService,
                             IEventAggregator eventAggregator, InquirySummaryViewModel viewModel)
        {
            FullInquiry = inquiry ?? throw new ArgumentNullException(nameof(inquiry));
            _inquiryService = inquiryService ?? throw new ArgumentNullException(nameof(inquiryService));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _inquiryViewModel = viewModel;

            var title = $"{FullInquiry.ClientKorisnikIdKorisnikNavigation.KorisnikIdKorisnikNavigation.Username} - {FullInquiry.OfferIdOfferNavigation.RealestateIdRealestateNavigation.AdresaIdAdresaNavigation.Street} {FullInquiry.OfferIdOfferNavigation.RealestateIdRealestateNavigation.AdresaIdAdresaNavigation.Number} ({FullInquiry.OfferIdOfferNavigation.Title})";
            Title = title;
            Summary = FullInquiry.Message;
            Subject = !string.IsNullOrWhiteSpace(title)
            ? $"Re: {title}"
                : string.Empty;
        }

        public ReplyViewModel()
        {
            _inquiry = null;
            Subject = "Re: ";
        }

        public async Task Send()
        {
            if (!CanSend) return;
            await TryCloseAsync(true);
            _inquiryViewModel.Delete(false);

        }

        public Task Cancel()
        {
            return TryCloseAsync(false);
        }
    }

}