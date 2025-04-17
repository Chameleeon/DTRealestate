using Nekretnine.Models;

namespace Nekretnine.Services
{
    public interface IInquiryService
    {
        Task<List<Inquiry>> GetAllInquiriesAsync();
        Task<Inquiry> GetInquiryByIdAsync(int id);
        Task<List<Inquiry>> GetUserInquiriesAsync(int userId);
        Task CreateInquiryAsync(Inquiry inquiry);
        Task DeleteInquiryAsync(int inquiryId);

        int GetCount();
    }
}