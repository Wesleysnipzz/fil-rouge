using Microsoft.Extensions.Logging;
using Shared.Data;
using System.Collections.Generic;
using System.Linq;

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

        public List<Forme> ObtenirEchiquier()
        {
            return _context.Formes.ToList();
        }
    }
}
