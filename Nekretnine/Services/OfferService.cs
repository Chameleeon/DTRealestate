using Microsoft.EntityFrameworkCore;
using Nekretnine.Data;
using Nekretnine.Models;
public interface IOfferService
{
    Task<List<Offer>> GetAllOffersAsync();
    Task<IEnumerable<Realestate>> GetRealestateOptionsAsync();
    Task<IEnumerable<Offertype>> GetOfferTypeOptionsAsync();
    Task SaveOfferAsync(Offer offer);
    Task<bool> UpdateOfferAsync(Offer offer);
    int GetCount();
}

public class OfferService : IOfferService
{
    private readonly RealEstateDbContext _context;

    public OfferService(RealEstateDbContext context)
    {
        _context = context;
    }

    public async Task<List<Offer>> GetAllOffersAsync()
    {
        return await _context.Offers.Include(o => o.RealestateIdRealestateNavigation).Include(o => o.TipIdTipNavigation).ToListAsync();
    }
    public async Task<IEnumerable<Realestate>> GetRealestateOptionsAsync()
    {
        return await _context.Realestates.ToListAsync();
    }

    public async Task<IEnumerable<Offertype>> GetOfferTypeOptionsAsync()
    {
        return await _context.Offertypes.ToListAsync();
    }

    public async Task SaveOfferAsync(Offer offer)
    {
        await _context.Offers.AddAsync(offer);
        await _context.SaveChangesAsync();
    }
    public int GetCount()
    {
        return _context.Offers.Count();
    }

    public async Task<bool> UpdateOfferAsync(Offer offer)
    {
        try
        {
            var existingOffer = await _context.Offers.FindAsync(offer.IdOffer);

            if (existingOffer == null)
            {
                return false;
            }

            existingOffer.Title = offer.Title;
            existingOffer.ShortDescription = offer.ShortDescription;
            existingOffer.Price = offer.Price;
            existingOffer.TipIdTip = offer.TipIdTip;

            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
