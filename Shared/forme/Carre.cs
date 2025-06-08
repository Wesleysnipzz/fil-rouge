namespace Shared.forme;

public class Carre : Forme
{
    public double Cote { get; set; }

    public Carre(double cote, string position) : base(5)
    {
        Cote = cote;
        this.position = position; // Utilise la propriété héritée, pas une nouvelle déclaration
    }

    public override double GetPerimetre() => 4 * Cote;
    public override double GetAire() => Cote * Cote;
}