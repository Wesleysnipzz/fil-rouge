using EzChess.forme;
using EzChess;
using Xunit;
using Moq; 

namespace TestProject;

public class UnitTest1
{
    [Fact]
    public void TestCercle()
    {
        double rayon = 5;
        var cercle = new Cercle(rayon);
        var expectedPerimetre = 2 * Math.PI * rayon;
        var expectedAire = Math.PI * rayon * rayon;

        Assert.Equal(expectedPerimetre, cercle.GetPerimetre(), 5);
        Assert.Equal(expectedAire, cercle.GetAire(), 5);
    }

    [Fact]
    public void TestRectangle()
    {
        double longueur = 5;
        double largeur = 6;
        var rectangle = new Rectangle(longueur, largeur);
        var expectedPerimetre = 2 * (longueur + largeur);
        var expectedAire = longueur * largeur;

        Assert.Equal(expectedPerimetre, rectangle.GetPerimetre(), 5);
        Assert.Equal(expectedAire, rectangle.GetAire(), 5);
    }

    [Fact]
    public void TestCarre()
    {
        double cote = 5;
        var carre = new Carre(cote);
        var expectedPerimetre = 4 * cote;
        var expectedAire = cote * cote;

        Assert.Equal(expectedPerimetre, carre.GetPerimetre(), 5);
        Assert.Equal(expectedAire, carre.GetAire(), 5);
    }

    [Fact]
    public void TestTriangle()
    {
        double cote = 3;
        var triangle = new Triangle(cote);
        var expectedPerimetre = 3 * cote;
        var expectedAire = (Math.Sqrt(3) / 4) * cote * cote;

        Assert.Equal(expectedPerimetre, triangle.GetPerimetre(), 5);
        Assert.Equal(expectedAire, triangle.GetAire(), 5);
    }

    [Fact]
    public void TestChessBoard()
    {
        var chessBoard = new ChessBoard();
        chessBoard.AjouterForme(new Rectangle(5, 3)); // on va simuler un mock por remplacer ajouterforme 
        chessBoard.AjouterForme(new Cercle(3)); // le mock va prendre le relais lors d'un test unitaire
    }
}

public class GameManagerTests
{
    [Fact]
    public void Test_PlacementForme()
    {
        // Arrange
        var gameManager = new GameManager();
        var carre = new Carre(4); // Crée un carré de 4 de côté

        // Act
        gameManager.PlacerForme("A1", carre); // Place le carré à la position A1
        var result = gameManager.GetForme("A1"); // Récupère la forme de la position A1

        // Assert
        Assert.NotNull(result); // Vérifie que la forme n'est pas null
        Assert.IsType<Carre>(result); // Vérifie que c'est bien un carré
    }

    [Fact]
    public void Test_GetForme_CaseVide()
    {
        // Arrange
        var gameManager = new GameManager();

        // Act
        var result = gameManager.GetForme("H8");

        // Assert
        Assert.Null(result); // Vérifie qu'il n'y a aucune forme à cette position
    }

    [Fact]
    public void Test_AfficherEchiquier()
    {
        // Arrange
        var gameManager = new GameManager();
        gameManager.PlacerForme("A1", new Carre(4));

        // Act
        var exception = Record.Exception(() => gameManager.AfficherEchiquier()); // Vérifie si la méthode s'exécute sans erreurs

        // Assert
        Assert.Null(exception); // Vérifie qu'il n'y a pas d'exception pendant l'affichage
    }

    [Fact]
    public void Test_EchiquierInitial()
    {
        // Arrange
        var gameManager = new GameManager();

        // Act
        var resultA1 = gameManager.GetForme("A1");
        var resultH8 = gameManager.GetForme("H8");

        // Assert
        Assert.Null(resultA1); // La case A1 doit être vide au départ
        Assert.Null(resultH8); // La case H8 doit être vide au départ
    }
    [Fact]
    public void Test_AfficherFormes()
    {
        // Arrange
        var chessBoard = new ChessBoard();
        chessBoard.AjouterForme(new Carre(4));
        chessBoard.AjouterForme(new Rectangle(3, 5));
        chessBoard.AjouterForme(new Cercle(2));

        var sortieOriginale = Console.Out;
        using var sortieCapturee = new StringWriter();
        Console.SetOut(sortieCapturee);

        try
        {
            // Act
            chessBoard.AfficherFormes();
            string resultat = sortieCapturee.ToString();

            // Assert
            Assert.Contains("Formes présentes sur l'échiquier", resultat);
            Assert.Contains("Carre", resultat);
            Assert.Contains("Rectangle", resultat);
            Assert.Contains("Cercle", resultat);
            Assert.Contains("Périmètre:", resultat);
            Assert.Contains("Aire:", resultat);
        }
        finally
        {
            // Restaurer la sortie console
            Console.SetOut(sortieOriginale);
        }
    }
    [Fact]
    public void TestMockAjouter_forme()
    {
        
        var mockChessBoard = new Mock<ChessBoard>();

        
        var gameManager = new GameManager(chessBoard: mockChessBoard.Object);
        
        var carre = new Carre(4);
        gameManager.PlacerForme("E4", carre);

        
        mockChessBoard.Verify(cb => cb.AjouterForme(It.IsAny<Forme>()), Times.Once());
    }

}
