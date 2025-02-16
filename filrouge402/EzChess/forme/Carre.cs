namespace EzChess.forme;

public class Carre : Forme
{
    public double Cote { get; set; }

    public Carre(double cote) : base(0)
    {
        Cote = cote;
    }

    public override double GetPerimetre()
    {
        return 4 * Cote;
    }

    public override double GetAire()
    {
        return Cote * Cote;
    }
}