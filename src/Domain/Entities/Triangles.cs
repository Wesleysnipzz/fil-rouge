using System;

namespace monTriangle
{
    public class Triangle : Forme
    {
     
        public double T { get; set; }

        public Triangle(double l) : base(0)
        {
            T = l;
        }

        public override double getPerimetre()
        {
            return T * 3;
        }

        public override double getAire()
        {
     
            return (Math.Sqrt(3) / 4) * T * T;
        }
    }
}