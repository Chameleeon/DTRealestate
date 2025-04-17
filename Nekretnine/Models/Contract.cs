using System;
using System.Collections.Generic;

namespace Nekretnine.Models;

public partial class Contract
{
    public int IdContract { get; set; }

    public string Content { get; set; } = null!;

    public DateOnly SignDate { get; set; }

    public int PeriodVazenja { get; set; }

    public int OfferIdOffer { get; set; }

    public int ClientKorisnikIdKorisnik { get; set; }

    public virtual Client ClientKorisnikIdKorisnikNavigation { get; set; } = null!;

    public virtual Offer OfferIdOfferNavigation { get; set; } = null!;
}
