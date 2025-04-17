using Caliburn.Micro;
using Nekretnine.Models;
using Nekretnine.Services;


namespace Nekretnine.ViewModels
{
    public class InquiriesViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private readonly IInquiryService _inquiryService;

        public BindableCollection<InquirySummaryViewModel> Inquiries { get; set; } = new();

        private InquirySummaryViewModel _selectedInquiry;
        public InquirySummaryViewModel SelectedInquiry
        {
            get => _selectedInquiry;
            set
            {
                _selectedInquiry = value;
                NotifyOfPropertyChange(() => SelectedInquiry);
                if (value != null)
                    OpenInquiryDetails(value.FullInquiry);
            }
        }

        public InquiriesViewModel(IInquiryService inquiryService, IWindowManager windowManager)
        {
            _inquiryService = inquiryService;
            _windowManager = windowManager;
            LoadInquiries();
        }

        private async void LoadInquiries()
        {
            var inquiries = await _inquiryService.GetAllInquiriesAsync();

            foreach (var inquiry in inquiries)
            {
                var offer = inquiry.OfferIdOfferNavigation;
                var user = inquiry.ClientKorisnikIdKorisnikNavigation;
                var address = offer.RealestateIdRealestateNavigation.AdresaIdAdresaNavigation;

                var title = $"{user.KorisnikIdKorisnikNavigation.Username} - {address.Street} {address.Number} ({offer.Title})";

                var summaryVm = IoC.Get<InquirySummaryViewModel>();

                summaryVm.Title = title;
                summaryVm.Summary = inquiry.Message;
                summaryVm.FullInquiry = inquiry;

                Inquiries.Add(summaryVm);
            }
        }

        private void OpenInquiryDetails(Inquiry inquiry)
        {
            //var vm = new InquiryDetailViewModel(inquiry);
            //_windowManager.ShowDialogAsync(vm);
        }
    }
}