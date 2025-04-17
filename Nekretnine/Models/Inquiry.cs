using System;
using System.Collections.Generic;

namespace Nekretnine.Models;

public partial class Inquiry
{
    public int IdInquiry { get; set; }

    public string Message { get; set; } = null!;

    public int ClientKorisnikIdKorisnik { get; set; }

    public int OfferIdOffer { get; set; }

    public virtual Client ClientKorisnikIdKorisnikNavigation { get; set; } = null!;

    public virtual Offer OfferIdOfferNavigation { get; set; } = null!;
}
