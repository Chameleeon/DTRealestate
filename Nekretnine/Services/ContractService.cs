using Nekretnine.Models;
using Nekretnine.Repositories;

namespace Nekretnine.Services
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _repository;

        public ContractService(IContractRepository repository)
        {
            _repository = repository;
        }

        public async Task<Contract?> GetContractAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Contract>> GetAllContractsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task CreateContractAsync(Contract contract)
        {
            await _repository.AddAsync(contract);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateContractAsync(Contract contract)
        {
            _repository.Update(contract);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteContractAsync(int id)
        {
            var contract = await _repository.GetByIdAsync(id);
            if (contract != null)
            {
                _repository.Delete(contract);
                await _repository.SaveChangesAsync();
            }
        }
        public int GetCount()
        {
            return _repository.GetCount();
        }
    }
}
