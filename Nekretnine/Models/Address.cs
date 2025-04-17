using System;
using System.Collections.Generic;

namespace Nekretnine.Models;

public partial class Address
{
    public int IdAddress { get; set; }

    public string Number { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string City { get; set; } = null!;

    public virtual ICollection<Realestate> Realestates { get; set; } = new List<Realestate>();
}
