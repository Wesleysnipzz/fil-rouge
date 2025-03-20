namespace EzChess.forme;

public class Triangle(double cote) : Forme(9)
{
    public double Cote { get; set; } = cote;

    public override double GetPerimetre()
    {
        return 3 * Cote;
    }

    public override double GetAire()
    {
        return (Math.Sqrt(3) / 4) * Cote * Cote;
    }
}