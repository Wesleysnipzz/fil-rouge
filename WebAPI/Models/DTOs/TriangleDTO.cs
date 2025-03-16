namespace WebAPI.Models.DTOs;

public class TriangleDto
{
    public double Cote { get; set; }
    public double Perimetre { get; set; }  // Ajout
    public double Aire { get; set; }       // Ajout

    public TriangleDto() { }

    public TriangleDto(double cote)
    {
        Cote = cote;
    }
}