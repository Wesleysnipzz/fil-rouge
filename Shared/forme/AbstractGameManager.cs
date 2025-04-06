using Microsoft.Extensions.Logging;
using Shared.Data;
using Shared.Interface;

namespace Shared.forme
{
    public abstract class AbstractGameManager : IGameManager
    {
        protected readonly ApplicationDbContext _context;
        protected readonly ILogger _logger;

        protected AbstractGameManager(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        // Méthode abstraite que les sous-classes doivent implémenter
        protected abstract string[] GenererPositions();

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
                .FirstOrDefault(f => f.position.Equals(position, StringComparison.OrdinalIgnoreCase));

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
                .FirstOrDefault(f => f.position.Equals(position, StringComparison.OrdinalIgnoreCase));
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

        public Dictionary<string, object> ObtenirEchiquier()
        {
            var positions = GenererPositions();
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
                if (positions.Contains(forme.position))
                    echiquier[forme.position] = forme.GetType().Name;
            }

            return echiquier;
        }
    }
}