using EzChess.forme;

class Program
{
    static void Main(string[] args)
    {
     
        EzChess.ChessBoard chessBoard = new EzChess.ChessBoard();
        chessBoard.AfficherResultats();

        Carre carre = new Carre(4);
        Console.WriteLine("Périmètre du carré : " + carre.GetPerimetre() + " Aire du carré : " + carre.GetAire());

        Rectangle rectangle = new Rectangle(4, 5);
        Console.WriteLine("Périmètre du rectangle : " + rectangle.GetPerimetre() + " Aire du rectangle : " + rectangle.GetAire());

        Cercle cercle = new Cercle(4);
        Console.WriteLine("Périmètre du cercle : " + cercle.GetPerimetre() + " Aire du cercle : " + cercle.GetAire());

        Triangle triangle = new Triangle(3);
        Console.WriteLine("Périmètre du triangle : " + triangle.GetPerimetre() + " Aire du triangle : " + triangle.GetAire());
        // commentaire pour push la branch pro402-5
    }
}