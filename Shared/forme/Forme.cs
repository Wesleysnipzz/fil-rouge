namespace Shared.forme;

public abstract class Forme
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string position { get; set; } = "ValeurProvisoire";
    private int Valeur { get; set; } = default!;
    public int BoardId { get; set; } = 1; // Par défaut, l'échiquier 1

    protected Forme(int valeur)
    {
        Valeur = valeur;
    }
    
    public abstract double GetPerimetre();
    public abstract double GetAire();
    
    // Ajout des propriétés calculées pour l'affichage
    public double Aire => GetAire();
    public double Perimetre => GetPerimetre();
    public string Type => GetType().Name;
}
