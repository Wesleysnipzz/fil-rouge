namespace Shared.forme;

public class Triangle : Forme
{
    public double Cote { get; set; }

    public Triangle(double cote, string position) : base(9)
    {
        Cote = cote;
        this.position = position; // Affecte la propriété héritée
    }

    public override double GetPerimetre() => 3 * Cote;
    public override double GetAire() => (Math.Sqrt(3) / 4) * Cote * Cote;
}