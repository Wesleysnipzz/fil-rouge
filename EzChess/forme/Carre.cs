namespace EzChess.forme;

public class Carre(double cote) : Forme(0)
{
    private double Cote { get; set; } = cote;

    public override double GetPerimetre()
    {
        return 4 * Cote;
    }

    public override double GetAire()
    {
        return Cote * Cote;
    }
}