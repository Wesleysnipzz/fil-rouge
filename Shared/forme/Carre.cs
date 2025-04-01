namespace Shared.forme;

public class Carre(double cote , string position) : Forme(5)
{
    public double Cote { get; set; } = cote;

    public override double GetPerimetre()
    {
        return 4 * Cote;
    }

    public override double GetAire()
    {
        return Cote * Cote;
    }
}