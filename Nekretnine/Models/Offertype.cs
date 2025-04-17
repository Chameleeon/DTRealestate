namespace Nekretnine.Models;

public partial class Offertype
{
    public int IdType { get; set; }

    public string OfferType1 { get; set; } = null!;

    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();

    public override string ToString()
    {
        return IdType + " - " + OfferType1;
    }
}
