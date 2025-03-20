
namespace  EzChess.forme;

public abstract class Forme

{
    public Guid Id { get; set; } = Guid.NewGuid();
    private int Valeur;

    protected Forme(int valeur)
    {
        Valeur = valeur;
    }

    public int GetValeur()
    {
        return Valeur;
    }

    public abstract double GetPerimetre();
    public abstract double GetAire();
}
