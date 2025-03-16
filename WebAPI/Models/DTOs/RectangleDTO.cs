namespace WebAPI.Models.DTOs;

public class RectangleDto
{
    public double Longueur { get; set; }
    public double Largeur { get; set; }
    public double Perimetre { get; set; }  // Ajout
    public double Aire { get; set; }       // Ajout

    public RectangleDto() { }

    public RectangleDto(double longueur, double largeur)
    {
        Longueur = longueur;
        Largeur = largeur;
    }
}