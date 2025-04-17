using Nekretnine.Models;
using Nekretnine.Repositories;

namespace Nekretnine.Services
{
    public class InquiryService : IInquiryService
    {
        private readonly IInquiryRepository _repository;

        public InquiryService(IInquiryRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Inquiry>> GetAllInquiriesAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<Inquiry> GetInquiryByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<List<Inquiry>> GetUserInquiriesAsync(int userId)
        {
            return _repository.GetByUserIdAsync(userId);
        }

        public Task CreateInquiryAsync(Inquiry inquiry)
        {
            return _repository.AddAsync(inquiry);
        }

        public Task DeleteInquiryAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }
        public int GetCount()
        {
            return _repository.GetCount();
        }
    }

}
