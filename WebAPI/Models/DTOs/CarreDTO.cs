namespace WebAPI.Models.DTOs;

public class CarreDto
{
    public double Cote { get; set; }
    public double Perimetre { get; set; }  
    public double Aire { get; set; }       

    public CarreDto() { }

    public CarreDto(double cote)
    {
        Cote = cote;
    }
}