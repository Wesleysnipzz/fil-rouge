namespace Shared.forme;

public class Rectangle(double longueur, double largeur,  string position ) : Forme(1000)
{
    public double Longueur { get; set; } = longueur;
    public double Largeur { get; set; } = largeur;

    public override double GetPerimetre()
    {
        return 2 * (Longueur + Largeur);
    }

    public override double GetAire()
    {
        return Longueur * Largeur;
    }
}