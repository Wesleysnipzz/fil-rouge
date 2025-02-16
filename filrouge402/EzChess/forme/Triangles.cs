namespace EzChess.forme;

public class Triangle : Forme
{
    public double Cote { get; set; }

    public Triangle(double cote) : base(0)
    {
        Cote = cote;
    }

    public override double GetPerimetre()
    {
        return 3 * Cote;
    }

    public override double GetAire()
    {
        return (Math.Sqrt(3) / 4) * Cote * Cote;
    }
}