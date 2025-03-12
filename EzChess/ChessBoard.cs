using EzChess.forme;


namespace EzChess
{
    // ChessBoard ne va plus hériter de Forme, mais va gérer une liste de Formes
    public class ChessBoard
    {
        private readonly List<Forme> _mesFormes;

        public ChessBoard()
        {
            _mesFormes = new List<Forme>(); // Liste vide de formes
        }

        // Ajouter une forme au tableau
        public virtual void AjouterForme(Forme forme)
        {
            _mesFormes.Add(forme);
        }
        

        // Afficher les formes présentes sur l'échiquier
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