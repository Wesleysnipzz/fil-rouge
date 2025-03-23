using EzChess.forme;


namespace EzChess
{
    
    public class ChessBoard
    {
        private readonly List<Forme> _mesFormes;

        public ChessBoard()
        {
            _mesFormes = new List<Forme>(); 
        }

        
        public virtual void AjouterForme(Forme forme)
        {
            _mesFormes.Add(forme);
        }
        

        
        public void AfficherFormes()
        {
            Console.WriteLine("Formes présentes sur l'échiquier :");
            foreach (var forme in _mesFormes)
            {
                Console.WriteLine($"- {forme.GetType().Name} (Périmètre: {forme.GetPerimetre()}, Aire: {forme.GetAire()})");
            }
        }
    }
}