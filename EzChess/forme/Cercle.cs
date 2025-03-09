namespace EzChess.forme;

public class Cercle : Forme
{
    public double Rayon { get; set; }

    public Cercle(double rayon) : base(0)
    {
        Rayon = rayon;
    }

    public override double GetPerimetre()
    {
        return 2 * Math.PI * Rayon;
    }

    public override double GetAire()
    {
        return Math.PI * Rayon * Rayon;
    }
}