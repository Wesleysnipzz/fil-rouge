using EzChess;
using Microsoft.AspNetCore.Mvc;
using EzChess.forme;
using WebAPI.Models.DTOs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameApiController : ControllerBase
    {
        private readonly ILogger<GameApiController> _logger;
        private readonly EzChess.GameManager _gameManager;

        public GameApiController(ILogger<GameApiController> logger)
        {
            _logger = logger;
            _gameManager = new GameManager();
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

            bool placed = _gameManager.PlacerForme(position.ToUpperInvariant(), forme);
            if (!placed)
            {
                _logger.LogWarning($"Échec du placement de la forme à la position {position}.");
                return BadRequest($"Échec du placement sur l'échiquier en {position}");
            }

            _logger.LogInformation($"Forme {forme.GetType().Name} placée avec succès à la position {position}.");
            return Ok($"Forme {forme.GetType().Name} placée en {position}");
        }

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
            var board = _gameManager.GetEchiquier();
            _logger.LogInformation("Affichage de l'échiquier demandé.");
            return Ok(board);
        }
    }
}