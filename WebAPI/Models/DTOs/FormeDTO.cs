using Shared.forme;

namespace WebAPI.Models.DTOs
{
    public class FormeDto
    {
        public Guid Id { get; set; }
        public double Longueur { get; set; }
        public double Largeur { get; set; }
        public double Cote { get; set; }
        public double Rayon { get; set; }
        public double Perimetre { get; set; }
        public double Aire { get; set; }
        // Ajout de l'initialisation par défaut pour éviter les avertissements
        public string Type { get; set; } = string.Empty;
        public string position { get; set; } = string.Empty;

        // Constructeur sans paramètre pour permettre la désérialisation.
        public FormeDto() { }

        public FormeDto(Carre c)
        {
            Id = c.Id;
            Type = "Carre";
            Cote = c.Cote;
            Perimetre = c.GetPerimetre();
            Aire = c.GetAire();
            position = c.position;
        }

        public FormeDto(Rectangle r)
        {
            Id = r.Id;
            Type = "Rectangle";
            Longueur = r.Longueur;
            Largeur = r.Largeur;
            Perimetre = r.GetPerimetre();
            Aire = r.GetAire();
            position = r.position;
        }

        public FormeDto(Triangle t)
        {
            Id = t.Id;
            Type = "Triangle";
            Cote = t.Cote;
            Perimetre = t.GetPerimetre();
            Aire = t.GetAire();
            position = t.position;
        }

        public FormeDto(Cercle c)
        {
            Id = c.Id;
            Type = "Cercle";
            Rayon = c.Rayon;
            Perimetre = c.GetPerimetre();
            Aire = c.GetAire();
            position = c.position;
        }

        public FormeDto(Forme forme)
        {
            if (forme == null)
                throw new ArgumentNullException(nameof(forme));

            Id = forme.Id;
            Perimetre = forme.GetPerimetre();
            Aire = forme.GetAire();

            switch (forme)
            {
                case Carre carre:
                    Type = "Carre";
                    Cote = carre.Cote;
                    break;
                case Rectangle rect:
                    Type = "Rectangle";
                    Longueur = rect.Longueur;
                    Largeur = rect.Largeur;
                    break;
                case Triangle triangle:
                    Type = "Triangle";
                    Cote = triangle.Cote;
                    break;
                case Cercle cercle:
                    Type = "Cercle";
                    Rayon = cercle.Rayon;
                    break;
                default:
                    Type = forme.ToString();
                    break;
            }
        }
    }
}
