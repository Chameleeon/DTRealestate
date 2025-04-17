using Caliburn.Micro;
using Nekretnine.Models;

namespace Nekretnine.ViewModels
{
    public class ContractItemViewModel : PropertyChangedBase
    {
        private readonly Contract _contract;
        private string _address;

        public Contract Contract => _contract;

        public int ContractId => _contract.IdContract;

        public DateOnly SignDate => _contract.SignDate;

        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                NotifyOfPropertyChange(() => Address);
            }
        }

        public ContractItemViewModel(Contract contract, string address)
        {
            _contract = contract;
            _address = address;
        }
    }
}
