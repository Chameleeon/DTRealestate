using System;
using System.Collections.Generic;

namespace Nekretnine.Models;

public partial class Administrator
{
    public int KorisnikIdKorisnik { get; set; }

    public virtual User KorisnikIdKorisnikNavigation { get; set; } = null!;
}
