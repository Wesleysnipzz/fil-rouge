using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.DTOs;
using Shared.forme;
using Shared.Data;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameApiController : ControllerBase
    {
        private readonly ILogger<GameApiController> _logger;
        private readonly GameManager _gameManager;
        private readonly ApplicationDbContext _contexte;

        public GameApiController(ILogger<GameApiController> logger, GameManager gameManager, ApplicationDbContext contexte)
        {
            _logger = logger;
            _gameManager = gameManager;
            _contexte = contexte;
        }

        [HttpPost("{position}")]
        public IActionResult Game(string position, [FromBody] FormeDto formeDto)
        {
            if (formeDto == null)
            {
                _logger.LogWarning("Tentative de placement d'une forme avec des données nulles.");
                return BadRequest("Données invalides");
            }

            Forme forme;
            switch (formeDto.Type.ToLower())
            {
                case "carre":
                    forme = new Carre(formeDto.Cote, formeDto.position);
                    break;
                case "rectangle":
                    forme = new Rectangle(formeDto.Longueur, formeDto.Largeur, formeDto.position);
                    break;
                case "triangle":
                    forme = new Triangle(formeDto.Cote, formeDto.position);
                    break;
                case "cercle":
                    forme = new Cercle(formeDto.Rayon, formeDto.position);
                    break;
                default:
                    _logger.LogWarning($"Type de forme inconnu: {formeDto.Type}");
                    return BadRequest("Type de forme inconnu");
            }

            var placed = _gameManager.PlacerForme(position, forme);
            
            if (!placed)
            {
                _logger.LogWarning($"Échec du placement de la forme à la position {position}.");
                return BadRequest($"Échec du placement sur l'échiquier en {position}");
            }

            // Vérification que la position est bien présente dans la base de données
            var formeEnregistree = _contexte.Formes
                .FirstOrDefault(f => f.position.Equals(position.ToUpperInvariant(), StringComparison.OrdinalIgnoreCase));
            if (formeEnregistree == null)
            {
                _logger.LogError($"La position {position} n'est pas présente dans la base de données après placement.");
                return StatusCode(500, $"Erreur interne : La position {position} n'a pas été enregistrée.");
            }

            _logger.LogInformation($"Forme enregistrée à la position {formeEnregistree.position} dans la base de données.");
            return Ok("Placement réussi");
        } // Ajout de la fermeture de la méthode Game

        [HttpDelete("{position}")]
        
        public IActionResult DeletePiece(string position)
        {
            bool deleted = _gameManager.SupprimerForme(position);
            if (!deleted)
            {
                _logger.LogWarning($"Aucune forme supprimée à la position {position}.");
                return NotFound($"Aucune forme trouvée en {position} à supprimer.");
            }
            _logger.LogInformation($"Forme supprimée avec succès de la position {position}.");
            return Ok($"Forme supprimée de la position {position}");
        }

        [HttpPut("{position}")]
        public IActionResult UpdatePiece(string position, [FromBody] FormeDto formeDto)
        {
            if (formeDto == null)
            {
                _logger.LogWarning("Tentative de mise à jour avec des données nulles.");
                return BadRequest("Données invalides");
            }

            Forme nouvelleForme;
            switch (formeDto.Type.ToLower())
            {
                case "carre":
                    nouvelleForme = new Carre(formeDto.Cote, formeDto.position);
                    break;
                case "rectangle":
                    nouvelleForme = new Rectangle(formeDto.Longueur, formeDto.Largeur, formeDto.position);
                    break;
                case "triangle":
                    nouvelleForme = new Triangle(formeDto.Cote, formeDto.position);
                    break;
                case "cercle":
                    nouvelleForme = new Cercle(formeDto.Rayon, formeDto.position);
                    break;
                default:
                    _logger.LogWarning($"Type de forme inconnu: {formeDto.Type}");
                    return BadRequest("Type de forme inconnu");
            }

            bool modified = _gameManager.ModifierForme(position, nouvelleForme);
            if (!modified)
            {
                _logger.LogWarning($"Échec de la modification à la position {position}.");
                return NotFound($"La position {position} n'existe pas ou n'a pas pu être modifiée.");
            }
            _logger.LogInformation($"Forme à la position {position} mise à jour avec {nouvelleForme.GetType().Name}.");
            return Ok($"La position {position} a été mise à jour.");
        }

        [HttpGet("board")]
        public IActionResult GetBoard()
        {
            var board = _gameManager.ObtenirEchiquier();
            _logger.LogInformation("Affichage de l'échiquier demandé.");
            return Ok(board);
        }
        
        [HttpGet("echequier")]
        public IActionResult GetEchiquier()
        {
            var board = _gameManager.ObtenirEchiquier();
            _logger.LogInformation("Affichage de l'échiquier demandé.");
            return Ok(board);
        }
    }
}
