namespace EzChess.forme;

public class Cercle(double rayon) : Forme(0)
{
    private double Rayon { get; set; } = rayon;

    public override double GetPerimetre()
    {
        return 2 * Math.PI * Rayon;
    }

    public override double GetAire()
    {
        return Math.PI * Rayon * Rayon;
    }
}