using System;
using EzChess;
using EzChess.forme;
using Xunit;

namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public void TestCercle()
        {
            double rayon = 5;
            Cercle cercle = new Cercle(rayon);
            double expectedPerimetre = 2 * Math.PI * rayon;
            double expectedAire = Math.PI * rayon * rayon;

            // Comparaison avec un delta de tolérance
            Assert.Equal(expectedPerimetre, cercle.GetPerimetre(), 5);
            Assert.Equal(expectedAire, cercle.GetAire(), 5);
        }

        [Fact]
        public void TestRectangle()
        {
            double longueur = 5;
            double largeur = 6;
            Rectangle rectangle = new Rectangle(longueur, largeur);
            double expectedPerimetre = 2 * (longueur + largeur);
            double expectedAire = longueur * largeur;

            Assert.Equal(expectedPerimetre, rectangle.GetPerimetre(), 5);
            Assert.Equal(expectedAire, rectangle.GetAire(), 5);
        }

        [Fact]
        public void TestCarre()
        {
            double cote = 5;
            Carre carre = new Carre(cote);
            double expectedPerimetre = 4 * cote;
            double expectedAire = cote * cote;

            Assert.Equal(expectedPerimetre, carre.GetPerimetre(), 5);
            Assert.Equal(expectedAire, carre.GetAire(), 5);
        }

        [Fact]
        public void TestTriangle()
        {
            double cote = 3;
            Triangle triangle = new Triangle(cote);
            double expectedPerimetre = 3 * cote;
            double expectedAire = (Math.Sqrt(3) / 4) * cote * cote;

            Assert.Equal(expectedPerimetre, triangle.GetPerimetre(), 5);
            Assert.Equal(expectedAire, triangle.GetAire(), 5);
        }

        [Fact]
        public void TestChessBoard()
        {
            EzChess.ChessBoard chessBoard = new EzChess.ChessBoard();
            double totalPerimetre = 0;
            double totalAire = 0;
            Forme[] formes = {
                new Rectangle(5, 3),
                new Triangle(4),
                new Carre(2),
                new Cercle(3)
            };

            foreach (var forme in formes)
            {
                totalPerimetre += forme.GetPerimetre();
                totalAire += forme.GetAire();
            }

            Assert.Equal(totalPerimetre, chessBoard.GetPerimetre(), 5);
            Assert.Equal(totalAire, chessBoard.GetAire(), 5);
        }
    }
}
