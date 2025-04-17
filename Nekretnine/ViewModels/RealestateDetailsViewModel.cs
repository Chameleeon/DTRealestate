using System.Windows.Input;
using Caliburn.Micro;
using Nekretnine.Models;

namespace Nekretnine.ViewModels
{
    public class RealestateDetailsViewModel : Screen
    {
        private readonly INavigationService _navigationService;

        public RealestateDetailsViewModel(Realestate realestate)
        {
            LoadFromModel(realestate);
        }

        public ICommand CancelCommand { get; }

        public string Description { get; set; }
        public string SquareFootage { get; set; }
        public string SelectedRealestateType { get; set; }
        public Address Address { get; set; }

        public void LoadFromModel(Realestate realestate)
        {
            Description = realestate.Description;
            SquareFootage = realestate.SquareFootage;
            SelectedRealestateType = realestate.TipNekretnineIdTipNekretnineNavigation.RealestateType1;
            Address = realestate.AdresaIdAdresaNavigation;

            NotifyOfPropertyChange(nameof(Description));
            NotifyOfPropertyChange(nameof(SquareFootage));
            NotifyOfPropertyChange(nameof(SelectedRealestateType));
            NotifyOfPropertyChange(nameof(Address));
        }

        public async Task Close()
        {
            await TryCloseAsync(false);
        }
    }
}
