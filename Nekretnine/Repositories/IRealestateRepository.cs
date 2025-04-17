using Nekretnine.Models;

namespace Nekretnine.Repositories
{
    public interface IRealestateRepository
    {
        Task<List<Realestate>> GetAllAsync();
        Task<Realestate> GetByIdAsync(int id);
        Task<bool> AddAsync(Realestate realestate);
        Task<bool> UpdateAsync(Realestate realestate);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Realestatetype>> GetRealestateTypesAsync();
    }
}