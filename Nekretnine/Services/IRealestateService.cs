using Nekretnine.Models;

namespace Nekretnine.Services
{
    public interface IRealestateService
    {
        Task<List<Realestate>> GetAllRealestatesAsync();
        Task<Realestate> GetRealestateByIdAsync(int id);
        Task<bool> AddRealestateAsync(Realestate realestate);
        Task<bool> UpdateRealestateAsync(Realestate realestate);
        Task<bool> DeleteRealestateAsync(int id);
        Task<IEnumerable<Realestatetype>> GetRealesTypeOptionsAsync();

    }
}