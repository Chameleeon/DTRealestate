using Microsoft.EntityFrameworkCore;
using Nekretnine.Data;
using Nekretnine.Models;

namespace Nekretnine.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly RealEstateDbContext _context;

        public ContractRepository(RealEstateDbContext context)
        {
            _context = context;
        }

        public async Task<Contract?> GetByIdAsync(int id)
        {
            return await _context.Contracts
                .Include(c => c.ClientKorisnikIdKorisnikNavigation)
                .Include(c => c.OfferIdOfferNavigation)
                .FirstOrDefaultAsync(c => c.IdContract == id);
        }

        public async Task<IEnumerable<Contract>> GetAllAsync()
        {
            return await _context.Contracts
                .Include(c => c.ClientKorisnikIdKorisnikNavigation)
                .Include(c => c.OfferIdOfferNavigation)
                .Include(c => c.OfferIdOfferNavigation.RealestateIdRealestateNavigation)
                .Include(c => c.OfferIdOfferNavigation.RealestateIdRealestateNavigation.AdresaIdAdresaNavigation)
                .Include(c => c.OfferIdOfferNavigation.TipIdTipNavigation)
                .ToListAsync();
        }

        public async Task AddAsync(Contract contract)
        {
            await _context.Contracts.AddAsync(contract);
        }

        public void Update(Contract contract)
        {
            _context.Contracts.Update(contract);
        }

        public void Delete(Contract contract)
        {
            _context.Contracts.Remove(contract);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public int GetCount()
        {
            var contracts = _context.Contracts.ToList();
            return contracts.Count;
        }
    }
}
