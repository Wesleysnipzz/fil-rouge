using  System;

namespace monCercle
{
    public class Cercle : Forme
    {
     
        public double R { get; set; }

       
        public Cercle(double r) : base(0)
        {
            R = r;
        }

        
        public override double getPerimetre()
        {
            return 2 * Math.PI * R;
        }

      
        public override double getAire()
        {
            return Math.PI * R * R;
        }
    }
}