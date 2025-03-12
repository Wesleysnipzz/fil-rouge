
using EzChess;
using EzChess.forme;

namespace Main
{
    public static class Program
    {
        public static void Main()
        {
            TestFormes.Run();
            TestGameManager.Run();
        }
    }

    public static class TestFormes
    {
        public static void Run()
        {
            Console.WriteLine("==== Test des Formes ====");
            
            ChessBoard chessBoard = new ChessBoard();
            chessBoard.AjouterForme(new Rectangle(4, 5));
            chessBoard.AjouterForme(new Carre(4));
            chessBoard.AjouterForme(new Cercle(4));
            chessBoard.AjouterForme(new Triangle(3));
            chessBoard.AfficherFormes();
            Console.WriteLine();

            Console.WriteLine("Détails individuels des formes :");

            Carre carre = new Carre(4);
            Console.WriteLine($"Carré - Périmètre : {carre.GetPerimetre()}, Aire : {carre.GetAire()}");

            Rectangle rectangle = new Rectangle(4, 5);
            Console.WriteLine($"Rectangle - Périmètre : {rectangle.GetPerimetre()}, Aire : {rectangle.GetAire()}");

            Cercle cercle = new Cercle(4);
            Console.WriteLine($"Cercle - Périmètre : {cercle.GetPerimetre()}, Aire : {cercle.GetAire()}");

            Triangle triangle = new Triangle(3);
            Console.WriteLine($"Triangle - Périmètre : {triangle.GetPerimetre()}, Aire : {triangle.GetAire()}");
            Console.WriteLine();
        }
    }

    public static class TestGameManager
    {
        public static void Run()
        {
            Console.WriteLine("==== Test du GameManager ====");
            GameManager gameManager = new GameManager();
            gameManager.PlacerForme("A1", new Carre(4));
            gameManager.PlacerForme("B2", new Cercle(3));
            gameManager.PlacerForme("C3", new Rectangle(4, 5));
            gameManager.AfficherEchiquier();
            Console.WriteLine();
        }
    }
}