namespace Shared.forme;

public class Cercle : Forme
{
    public double Rayon { get; set; }

    public Cercle(double rayon, string position) : base(1)
    {
        Rayon = rayon;
        this.position = position; // Affecte la position héritée
    }

    public override double GetPerimetre() => 2 * Math.PI * Rayon;
    public override double GetAire() => Math.PI * Rayon * Rayon;
}