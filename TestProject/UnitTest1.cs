using EzChess.forme;
using EzChess;
using Xunit;
using Moq;
using System;

namespace TestProject
{
    public class UnitTest1
    {
        // Tests pour les Formes
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
    }

    public class GameManagerTests
    {
        [Fact]
        public void Test_GetForme_CaseVide()
        {
            // Arrange
            var gameManager = new GameManager();

            // Act
            var result = gameManager.GetForme("H8");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Test_AfficherEchiquier()
        {
            // Arrange
            var gameManager = new GameManager();
            gameManager.PlacerForme("A1", new Carre(4));

            // Act & Assert
            var exception = Record.Exception(() => gameManager.AfficherEchiquier());
            Assert.Null(exception);
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
            Assert.Null(resultA1);
            Assert.Null(resultH8);
        }
    }
}
