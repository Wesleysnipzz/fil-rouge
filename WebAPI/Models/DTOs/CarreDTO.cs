namespace WebAPI.Models.DTOs;

public class CarreDto
{
    public double Cote { get; set; }

    public CarreDto() { }

    public CarreDto(double cote)
    {
        Cote = cote;
    }
}