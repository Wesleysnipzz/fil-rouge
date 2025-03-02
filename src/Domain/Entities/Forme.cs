using System;

namespace monForme
{
    public abstract class Forme
    {
        protected int _valeur;

       
       
        public Forme(int valeur)
        {
            _valeur = valeur;
        }

        
        public abstract double getPerimetre();
        public abstract double getAire();
    }
}