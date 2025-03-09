

namespace  EzChess.forme;

public abstract class Forme
{
    protected int Valeur;

       
       
    public Forme(int valeur)
    {
        Valeur = valeur;
    }

        
    public abstract double GetPerimetre();
    public abstract double GetAire();
}