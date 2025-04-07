using Microsoft.Extensions.Logging;
using Shared.Data;
using Shared.Interface;

namespace Shared.forme
{
    public abstract class AbstractGameManager : IGameManager
    {
        protected readonly ApplicationDbContext _context;
        protected readonly ILogger<AbstractGameManager> _logger;

        protected AbstractGameManager(ApplicationDbContext context, ILogger<AbstractGameManager> logger)
        {
            _context = context;
            _logger = logger;
        }

        protected abstract string[] GenererPositions();

        public bool PlacerForme(string position, Forme forme, int boardId = 1)
        {
            // Vérifier si le board existe
            var boardExists = _context.Boards.Any(b => b.Id == boardId);
            if (!boardExists)
            {
                // Créer un nouveau board si nécessaire
                var board = new Board 
                { 
                    Id = boardId,
                    Name = $"Échiquier {boardId}",
                    Type = "standard",
                    CreatedAt = DateTime.UtcNow
                };
                _context.Boards.Add(board);
                _context.SaveChanges();
            }
    
            forme.position = position.ToUpperInvariant();
            forme.BoardId = boardId;
            _context.Formes.Add(forme);
            _context.SaveChanges();
            return true;
        }
        
        public bool ModifierForme(string position, Forme nouvelleForme, int boardId = 1)
        {
            // Conversion en minuscules pour la comparaison
            string positionLower = position.ToLower();
            
            var formeExistante = _context.Formes
                .FirstOrDefault(f => f.position.ToLower() == positionLower && f.BoardId == boardId);

            if (formeExistante == null)
            {
                _logger.LogWarning($"Aucune forme trouvée à la position {position} sur l'échiquier {boardId}.");
                return false;
            }

            _context.Formes.Remove(formeExistante);
            nouvelleForme.position = position.ToUpperInvariant();
            nouvelleForme.BoardId = boardId;
            _context.Formes.Add(nouvelleForme);
            _context.SaveChanges();
            _logger.LogInformation($"Forme à {position} mise à jour sur l'échiquier {boardId}.");
            return true;
        }

        public bool SupprimerForme(string position, int boardId = 1)
        {
            // Conversion en minuscules pour la comparaison
            string positionLower = position.ToLower();
            
            var forme = _context.Formes
                .FirstOrDefault(f => f.position.ToLower() == positionLower && f.BoardId == boardId);
                
            if (forme == null)
            {
                _logger.LogWarning($"Aucune forme trouvée à la position {position} sur l'échiquier {boardId}.");
                return false;
            }

            _context.Formes.Remove(forme);
            _context.SaveChanges();
            _logger.LogInformation($"Forme supprimée de la position {position} sur l'échiquier {boardId}.");
            return true;
        }

        public Dictionary<string, object> ObtenirEchiquier(int boardId = 1)
        {
            var positions = GenererPositions();
            var echiquier = new Dictionary<string, object>();

            // Initialisation des positions vides
            foreach (var position in positions)
            {
                echiquier[position] = null;
            }

            // Remplissage avec les formes existantes pour cet échiquier
            var formes = _context.Formes.Where(f => f.BoardId == boardId).ToList();
            foreach (var forme in formes)
            {
                if (positions.Contains(forme.position))
                    echiquier[forme.position] = forme.GetType().Name;
            }

            return echiquier;
        }
        
        public List<Board> GetAllBoards()
        {
            return _context.Boards.ToList();
        }
        
        public Board CreateBoard(string name, string type = "standard")
        {
            var board = new Board
            {
                Name = name,
                Type = type,
                CreatedAt = DateTime.UtcNow // Utilisation de UtcNow pour éviter les erreurs de fuseau horaire
            };
            
            _context.Boards.Add(board);
            _context.SaveChanges();
            
            return board;
        }
    }
}