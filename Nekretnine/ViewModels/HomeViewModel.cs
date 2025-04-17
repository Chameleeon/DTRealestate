using Caliburn.Micro;
using Nekretnine.Services;

namespace Nekretnine.ViewModels
{
    public class HomeViewModel : Screen
    {
        private readonly IInquiryService _inquiryService;
        private readonly IOfferService _offerService;
        private readonly IContractService _contractService;

        private int _inquiryCount;
        public int InquiryCount
        {
            get => _inquiryCount;
            set
            {
                _inquiryCount = value;
                NotifyOfPropertyChange(() => InquiryCount);
            }
        }

        private int _activeOfferCount;
        public int ActiveOffersCount
        {
            get => _activeOfferCount;
            set
            {
                _activeOfferCount = value;
                NotifyOfPropertyChange(() => ActiveOffersCount);
            }
        }

        private int _completedContractCount;
        public int CompletedContractsCount
        {
            get => _completedContractCount;
            set
            {
                _completedContractCount = value;
                NotifyOfPropertyChange(() => CompletedContractsCount);
            }
        }

        public HomeViewModel(IInquiryService inquiryService, IOfferService offerService, IContractService contractService)
        {
            _inquiryService = inquiryService;
            _offerService = offerService;
            _contractService = contractService;
            LoadData();
        }

        public void LoadData()
        {
            InquiryCount = _inquiryService.GetCount();
            ActiveOffersCount = _offerService.GetCount();
            CompletedContractsCount = _contractService.GetCount();
        }
    }
}
