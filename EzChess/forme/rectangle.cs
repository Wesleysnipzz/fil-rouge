namespace EzChess.forme;

public class Rectangle : Forme
{
    private double Longueur { get; set; }
    private double Largeur { get; set; }

    public Rectangle(double longueur, double largeur) : base(0)
    {
        Longueur = longueur;
        Largeur = largeur;
    }

    public override double GetPerimetre()
    {
        return 2 * (Longueur + Largeur);
    }

    public override double GetAire()
    {
        return Longueur * Largeur;
    }
}