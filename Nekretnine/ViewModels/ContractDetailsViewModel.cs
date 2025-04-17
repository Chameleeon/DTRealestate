using Caliburn.Micro;
using Nekretnine.Models;

namespace Nekretnine.ViewModels
{
    public class ContractDetailsViewModel : Screen
    {
        private readonly Contract _contract;

        public ContractDetailsViewModel(Contract contract)
        {
            _contract = contract;
            var adr = contract.OfferIdOfferNavigation.RealestateIdRealestateNavigation.AdresaIdAdresaNavigation;
            Address = adr.Street + " " + adr.Number + ", " + adr.City;
            DisplayName = $"Contract #{contract.IdContract} Details";
        }

        public int ContractId => _contract.IdContract;

        public DateOnly SignDate => _contract.SignDate;

        public string Address { get; set; }

        public string Content => _contract.Content;

        public void Close()
        {
            TryCloseAsync(false);
        }
    }
}
