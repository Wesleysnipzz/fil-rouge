namespace WebAPI.Models.DTOs;

public class CercleDto
{
    public double Rayon { get; set; }

    public CercleDto() { }

    public CercleDto(double rayon)
    {
        Rayon = rayon;
    }
}