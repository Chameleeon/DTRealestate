using Nekretnine.Models;

namespace Nekretnine.Repositories
{
    public interface IInquiryRepository
    {
        Task<List<Inquiry>> GetAllAsync();
        Task<Inquiry> GetByIdAsync(int id);
        Task<List<Inquiry>> GetByUserIdAsync(int userId);
        Task AddAsync(Inquiry inquiry);
        Task DeleteAsync(int id);
        int GetCount();
    }

}
