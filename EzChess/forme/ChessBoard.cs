namespace EzChess.forme 
{

    public class ChessBoard : Forme
    {
        private readonly List<Forme> _mesFormes;

     
        public ChessBoard() : base(0)
        {
            _mesFormes = new List<Forme>();
            
            // Exemple d'ajout de formes
            _mesFormes.Add(new Rectangle(5, 3));
            _mesFormes.Add(new Triangle(4));
            _mesFormes.Add(new Carre(2));
            _mesFormes.Add(new Cercle(3));
       
        }

    
        public override double GetPerimetre()
        {
            double perimetreTotal = 0;
            foreach (var forme in _mesFormes)
            {
                perimetreTotal += forme.GetPerimetre();
            }
            return perimetreTotal;
        }

   
        public override double GetAire()
        {
            double aireTotal = 0;
            foreach (var forme in _mesFormes)
            {
                aireTotal += forme.GetAire();
            }
            return aireTotal;
        }

        

        public void AjouterForme(Forme forme)
        {
            _mesFormes.Add(forme);
        }


        
        public void AfficherResultats()
        {
            Console.WriteLine("Périmètre total : " + GetPerimetre());
            Console.WriteLine("Aire totale : " + GetAire());
        }
    }

}