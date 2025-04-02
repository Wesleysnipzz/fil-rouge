namespace Shared.forme;

public class Rectangle : Forme
{
    public double Longueur { get; set; }
    public double Largeur { get; set; }

    public Rectangle(double longueur, double largeur, string position) : base(1000)
    {
        Longueur = longueur;
        Largeur = largeur;
        this.position = position; // Utilise la propriété héritée
    }

    public override double GetPerimetre() => 2 * (Longueur + Largeur);
    public override double GetAire() => Longueur * Largeur;
}