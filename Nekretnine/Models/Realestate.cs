using System;
using System.Collections.Generic;

namespace Nekretnine.Models;

public partial class Realestate
{
    public int IdRealestate { get; set; }

    public string Description { get; set; } = null!;

    public int AdresaIdAdresa { get; set; }

    public int TipNekretnineIdTipNekretnine { get; set; }

    public string SquareFootage { get; set; } = null!;

    public string ImagePaths { get; set; } = null!;

    public DateOnly DateAdded { get; set; }

    public virtual Address AdresaIdAdresaNavigation { get; set; } = null!;

    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();

    public virtual Realestatetype TipNekretnineIdTipNekretnineNavigation { get; set; } = null!;
}
