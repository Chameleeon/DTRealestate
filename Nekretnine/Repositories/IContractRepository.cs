using Nekretnine.Models;

namespace Nekretnine.Repositories
{
    public interface IContractRepository
    {
        Task<Contract?> GetByIdAsync(int id);
        Task<IEnumerable<Contract>> GetAllAsync();
        Task AddAsync(Contract contract);
        void Update(Contract contract);
        void Delete(Contract contract);
        Task SaveChangesAsync();
        int GetCount();
    }
}