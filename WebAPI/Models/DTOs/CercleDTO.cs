namespace WebAPI.Models.DTOs;

public class CercleDto
{
    public double Rayon { get; set; }
    public double Perimetre { get; set; }  // Ajout
    public double Aire { get; set; }       // Ajout

    public CercleDto() { }

    public CercleDto(double rayon)
    {
        Rayon = rayon;
    }
}
