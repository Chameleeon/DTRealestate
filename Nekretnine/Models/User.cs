using System;
using System.Collections.Generic;

namespace Nekretnine.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual Administrator? Administrator { get; set; }

    public virtual Agent? Agent { get; set; }

    public virtual Client? Client { get; set; }
}
