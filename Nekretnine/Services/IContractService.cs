using Nekretnine.Models;

namespace Nekretnine.Services
{
    public interface IContractService
    {
        Task<Contract?> GetContractAsync(int id);
        Task<IEnumerable<Contract>> GetAllContractsAsync();
        Task CreateContractAsync(Contract contract);
        Task UpdateContractAsync(Contract contract);
        Task DeleteContractAsync(int id);
        int GetCount();
    }
}
