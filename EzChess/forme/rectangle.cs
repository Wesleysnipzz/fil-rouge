namespace EzChess.forme;

public class Rectangle(double longueur, double largeur) : Forme(1000)
{
    private double Longueur { get; set; } = longueur;
    private double Largeur { get; set; } = largeur;

    public override double GetPerimetre()
    {
        return 2 * (Longueur + Largeur);
    }

    public override double GetAire()
    {
        return Longueur * Largeur;
    }
}