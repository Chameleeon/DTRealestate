namespace Nekretnine.Models;

public partial class Client
{
    public int KorisnikIdKorisnik { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();

    public virtual User KorisnikIdKorisnikNavigation { get; set; } = null!;

    public override string ToString()
    {
        return KorisnikIdKorisnikNavigation.FirstName + " " + KorisnikIdKorisnikNavigation.LastName + " - " + KorisnikIdKorisnikNavigation.Username;
    }
}
