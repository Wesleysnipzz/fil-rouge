using Xunit;
using Moq; 

namespace TestProject;
using Shared.forme;

public class UnitTest1
{
    [Fact]
    public void TestCercle()
    {
        var cercleMock = new Mock<Cercle>();
        cercleMock.Setup(c => c.GetPerimetre()).Returns(31.42);
        cercleMock.Setup(c => c.GetAire()).Returns(78.54);

        var cercle = cercleMock.Object;

        // Appel effectif des méthodes sans affectation pour éviter avertissement
        cercle.GetPerimetre();
        cercle.GetAire();

        // Vérifications après appel
        cercleMock.Verify(c => c.GetPerimetre(), Times.Once);
        cercleMock.Verify(c => c.GetAire(), Times.Once);
    }

    [Fact]
    public void TestRectangle()
    {
        var rectangleMock = new Mock<Rectangle>();
        rectangleMock.Setup(r => r.GetPerimetre()).Returns(20);
        rectangleMock.Setup(r => r.GetAire()).Returns(50);

        var rectangle = rectangleMock.Object;
        // Appel effectif des méthodes directement
        rectangle.GetPerimetre();
        rectangle.GetAire();

        // Vérifications après appel
        rectangleMock.Verify(r => r.GetPerimetre(), Times.Once);
        rectangleMock.Verify(r => r.GetAire(), Times.Once);
    }

    [Fact]
    public void TestCarre()
    {
        var carreMock = new Mock<Carre>();
        carreMock.Setup(c => c.GetPerimetre()).Returns(16);
        carreMock.Setup(c => c.GetAire()).Returns(25);

        var carre = carreMock.Object;
        // Appel effectif des méthodes directement
        carre.GetPerimetre();
        carre.GetAire();

        // Vérifications après appel
        carreMock.Verify(c => c.GetPerimetre(), Times.Once);
        carreMock.Verify(c => c.GetAire(), Times.Once);
    }
}
