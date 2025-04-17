using System;
using System.Collections.Generic;

namespace Nekretnine.Models;

public partial class Transaction
{
    public int KlijentKorisnikIdKorisnik { get; set; }

    public decimal Iznos { get; set; }

    public DateOnly Datum { get; set; }

    public int UgovorIdUgovor { get; set; }

    public int OfferIdOffer { get; set; }

    public virtual Client KlijentKorisnikIdKorisnikNavigation { get; set; } = null!;

    public virtual Offer OfferIdOfferNavigation { get; set; } = null!;

    public virtual Contract UgovorIdUgovorNavigation { get; set; } = null!;
}
