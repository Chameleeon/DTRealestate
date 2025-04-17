namespace Nekretnine.Models;

public partial class Agent
{
    public int KorisnikIdKorisnik { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public bool Activated { get; set; }

    public virtual User KorisnikIdKorisnikNavigation { get; set; } = null!;

    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();
}
