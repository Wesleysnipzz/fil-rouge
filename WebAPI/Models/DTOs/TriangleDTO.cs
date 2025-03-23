namespace WebAPI.Models.DTOs;

public class TriangleDto
{
    public double Cote { get; set; }
    public double Perimetre { get; set; }  
    public double Aire { get; set; }       

    public TriangleDto() { }

    public TriangleDto(double cote)
    {
        Cote = cote;
    }
}