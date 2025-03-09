namespace EzChess.forme;

public class Rectangle : Forme
{
    public double Longueur { get; set; }
    public double Largeur { get; set; }

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