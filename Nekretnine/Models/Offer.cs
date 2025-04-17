using System;
using System.Collections.Generic;

namespace Nekretnine.Models;

public partial class Offer
{
    public int IdOffer { get; set; }

    public int TipIdTip { get; set; }

    public int AgentKorisnikIdKorisnik { get; set; }

    public decimal Price { get; set; }

    public string ShortDescription { get; set; } = null!;

    public string Title { get; set; } = null!;

    public sbyte Active { get; set; }

    public int RealestateIdRealestate { get; set; }

    public DateOnly DateAdded { get; set; }

    public virtual Agent AgentKorisnikIdKorisnikNavigation { get; set; } = null!;

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();

    public virtual Realestate RealestateIdRealestateNavigation { get; set; } = null!;

    public virtual Offertype TipIdTipNavigation { get; set; } = null!;
}
