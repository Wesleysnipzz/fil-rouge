namespace Shared.forme;

public class Cercle(double rayon, string position) : Forme(1)
{
    public double Rayon { get; set; } = rayon;


    public override double GetPerimetre()
    {
        return 2 * Math.PI * Rayon;
    }

    public override double GetAire()
    {
        return Math.PI * Rayon * Rayon;
    }
}