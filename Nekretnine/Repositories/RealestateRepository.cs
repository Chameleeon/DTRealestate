using Microsoft.EntityFrameworkCore;
using Nekretnine.Data;
using Nekretnine.Models;

namespace Nekretnine.Repositories
{
    public class RealestateRepository : IRealestateRepository
    {
        private readonly RealEstateDbContext _context;

        public RealestateRepository(RealEstateDbContext context)
        {
            _context = context;
        }

        public async Task<List<Realestate>> GetAllAsync()
        {
            return await _context.Realestates.Include(r => r.TipNekretnineIdTipNekretnineNavigation).Include(r => r.AdresaIdAdresaNavigation).Include(r => r.Offers).ToListAsync();
        }

        public async Task<Realestate> GetByIdAsync(int id)
        {
            return await _context.Realestates.FindAsync(id);
        }

        public async Task<bool> AddAsync(Realestate realestate)
        {
            _context.Realestates.Add(realestate);
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateAsync(Realestate realestate)
        {
            _context.Entry(realestate).State = EntityState.Modified;
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var realestate = await _context.Realestates.FindAsync(id);
            if (realestate == null)
                return false;

            _context.Realestates.Remove(realestate);
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Realestatetype>> GetRealestateTypesAsync()
        {
            return await _context.Realestatetypes.ToListAsync();
        }
    }
}