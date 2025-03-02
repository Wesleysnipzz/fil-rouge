using System;

namespace monCarre
{
    public class Carre : Forme
    {
        public double C { get; set; }

        public Carre(double c) : base(0)
        {
            C = c;
        }

        public override double getPerimetre()
        {
            return 4 * C;
        }

        public override double getAire()
        {
            return C * C;
        }
    }
}















}