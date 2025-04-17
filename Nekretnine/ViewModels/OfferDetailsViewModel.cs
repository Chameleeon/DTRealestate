using System.Windows.Input;
using Caliburn.Micro;
using Nekretnine.Models;

namespace Nekretnine.ViewModels
{
    public class OfferDetailsViewModel : Screen
    {
        private readonly INavigationService _navigationService;

        public OfferDetailsViewModel(Offer offer)
        {
            LoadFromModel(offer);
        }

        public ICommand CancelCommand { get; }

        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Price { get; set; }
        public string SelectedOfferType { get; set; }

        public void LoadFromModel(Offer offer)
        {
            Title = offer.Title;
            ShortDescription = offer.ShortDescription;
            Price = "" + offer.Price;
            SelectedOfferType = offer.TipIdTipNavigation.OfferType1;

            NotifyOfPropertyChange(nameof(Title));
            NotifyOfPropertyChange(nameof(ShortDescription));
            NotifyOfPropertyChange(nameof(Price));
            NotifyOfPropertyChange(nameof(SelectedOfferType));
        }

        public async Task Close()
        {
            await TryCloseAsync(false);
        }
    }
}
