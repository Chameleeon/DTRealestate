using Microsoft.EntityFrameworkCore;
using Nekretnine.Data;
using Nekretnine.Models;

namespace Nekretnine.Repositories
{
    public class InquiryRepository : IInquiryRepository
    {
        private readonly RealEstateDbContext _context;

        public InquiryRepository(RealEstateDbContext context)
        {
            _context = context;
        }

        public async Task<List<Inquiry>> GetAllAsync()
        {
            return await _context.Inquiries
                .Include(i => i.OfferIdOfferNavigation)
                    .ThenInclude(o => o.RealestateIdRealestateNavigation)
                        .ThenInclude(r => r.AdresaIdAdresaNavigation)
                .Include(i => i.ClientKorisnikIdKorisnikNavigation)
                .Include(i => i.ClientKorisnikIdKorisnikNavigation.KorisnikIdKorisnikNavigation)
                .ToListAsync();
        }

        public async Task<Inquiry> GetByIdAsync(int id)
        {
            return await _context.Inquiries
                .Include(i => i.OfferIdOfferNavigation)
                    .ThenInclude(o => o.RealestateIdRealestateNavigation)
                        .ThenInclude(r => r.AdresaIdAdresaNavigation)
                .Include(i => i.ClientKorisnikIdKorisnikNavigation)
                .FirstOrDefaultAsync(i => i.ClientKorisnikIdKorisnik == id);
        }

        public async Task AddAsync(Inquiry inquiry)
        {
            _context.Inquiries.Add(inquiry);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Inquiry>> GetByUserIdAsync(int userId)
        {
            return await _context.Inquiries
                .Include(i => i.OfferIdOfferNavigation)
                    .ThenInclude(o => o.RealestateIdRealestateNavigation)
                        .ThenInclude(r => r.AdresaIdAdresaNavigation)
                .Include(i => i.ClientKorisnikIdKorisnikNavigation)
                .Where(i => i.ClientKorisnikIdKorisnik == userId)
                .ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var inquiry = await _context.Inquiries.FindAsync(id);
            if (inquiry != null)
            {
                _context.Inquiries.Remove(inquiry);
                await _context.SaveChangesAsync();
            }
        }

        public int GetCount()
        {
            return _context.Inquiries.Count();
        }
    }

}
