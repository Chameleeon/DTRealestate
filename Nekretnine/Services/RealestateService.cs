using Nekretnine.Models;
using Nekretnine.Repositories;

namespace Nekretnine.Services
{
    public class RealestateService : IRealestateService
    {
        private readonly IRealestateRepository _realestateRepository;

        public RealestateService(IRealestateRepository realestateRepository)
        {
            _realestateRepository = realestateRepository;
        }

        public async Task<List<Realestate>> GetAllRealestatesAsync()
        {
            return await _realestateRepository.GetAllAsync();
        }

        public async Task<Realestate> GetRealestateByIdAsync(int id)
        {
            return await _realestateRepository.GetByIdAsync(id);
        }

        public async Task<bool> AddRealestateAsync(Realestate realestate)
        {
            return await _realestateRepository.AddAsync(realestate);
        }

        public async Task<bool> UpdateRealestateAsync(Realestate realestate)
        {
            return await _realestateRepository.UpdateAsync(realestate);
        }

        public async Task<bool> DeleteRealestateAsync(int id)
        {
            return await _realestateRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Realestatetype>> GetRealesTypeOptionsAsync()
        {
            return await _realestateRepository.GetRealestateTypesAsync();
        }
    }
}