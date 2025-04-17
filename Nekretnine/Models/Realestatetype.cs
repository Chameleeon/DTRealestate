namespace Nekretnine.Models;

public partial class Realestatetype
{
    public int IdRealesateType { get; set; }

    public string RealestateType1 { get; set; } = null!;

    public virtual ICollection<Realestate> Realestates { get; set; } = new List<Realestate>();

    public override string ToString()
    {
        return IdRealesateType + " - " + RealestateType1;
    }
}
