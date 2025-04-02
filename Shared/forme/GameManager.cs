using Microsoft.Extensions.Logging;
using Shared.Data;


namespace Shared.forme
{
    public class GameManager
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GameManager> _logger;

        public GameManager(ApplicationDbContext context, ILogger<GameManager> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool PlacerForme(string position, Forme forme)
        {
            forme.position = position.ToUpperInvariant();
            _context.Formes.Add(forme);
            _context.SaveChanges();
            _logger.LogInformation($"Forme {forme.GetType().Name} placée à {position}.");
            return true;
        }

        public bool ModifierForme(string position, Forme nouvelleForme)
        {
            var formeExistante = _context.Formes
                .FirstOrDefault(f => f.position.Equals(position, System.StringComparison.OrdinalIgnoreCase));

            if (formeExistante == null)
            {
                _logger.LogWarning($"Aucune forme trouvée à la position {position} pour mise à jour.");
                return false;
            }

            _context.Formes.Remove(formeExistante);
            nouvelleForme.position = position.ToUpperInvariant();
            _context.Formes.Add(nouvelleForme);
            _context.SaveChanges();
            _logger.LogInformation($"Forme à {position} mise à jour avec {nouvelleForme.GetType().Name}.");
            return true;
        }

        public bool SupprimerForme(string position)
        {
            var forme = _context.Formes
                .FirstOrDefault(f => f.position.Equals(position, System.StringComparison.OrdinalIgnoreCase));
            if (forme == null)
            {
                _logger.LogWarning($"Aucune forme trouvée à la position {position} pour suppression.");
                return false;
            }
            _context.Formes.Remove(forme);
            _context.SaveChanges();
            _logger.LogInformation($"Forme supprimée de la position {position}.");
            return true;
        }

        // Nouvelle méthode pour récupérer l'échiquier sous forme de dictionnaire JSON
        public Dictionary<string, object> ObtenirEchiquier()
        {
            var positions = new string[]
            {
                "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8",
                "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8",
                "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8",
                "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8",
                "E1", "E2", "E3", "E4", "E5", "E6", "E7", "E8",
                "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8",
                "G1", "G2", "G3", "G4", "G5", "G6", "G7", "G8",
                "H1", "H2", "H3", "H4", "H5", "H6", "H7", "H8"
            };

            var echiquier = new Dictionary<string, object>();

            // Initialisation des positions vides
            foreach (var position in positions)
            {
                echiquier[position] = null;
            }

            // Remplissage avec les formes existantes
            var formes = _context.Formes.ToList();
            foreach (var forme in formes)
            {
                echiquier[forme.position] = forme.GetType().Name;
            }

            return echiquier;
        }
    }
}
