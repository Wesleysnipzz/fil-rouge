using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.DTOs;
using Shared.forme;
using Shared.Interface;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameApiController : ControllerBase
    {
        private readonly ILogger<GameApiController> _logger;
        private readonly IGameManager _gameManager;

        public GameApiController(ILogger<GameApiController> logger, IGameManager gameManager)
        {
            _logger = logger;
            _gameManager = gameManager;
        }

        [HttpPost("{position}")]
        public IActionResult PlacerForme(string position, [FromBody] FormeDto formeDto, [FromQuery] int boardId = 1)
        {
            if (formeDto == null)
            {
                _logger.LogWarning("Tentative de placement d'une forme avec des données nulles.");
                return BadRequest("Données invalides");
            }

            Forme forme = CreerFormeDepuisDto(formeDto, position);
            if (forme == null)
            {
                _logger.LogWarning($"Type de forme inconnu: {formeDto.Type}");
                return BadRequest("Type de forme inconnu");
            }

            var placed = _gameManager.PlacerForme(position, forme, boardId);
            if (!placed)
            {
                _logger.LogWarning($"Échec du placement de la forme à la position {position} sur l'échiquier {boardId}.");
                return BadRequest($"Échec du placement sur l'échiquier {boardId} en {position}");
            }

            _logger.LogInformation($"Forme enregistrée à la position {position} sur l'échiquier {boardId}.");
            return Ok("Placement réussi");
        }

        [HttpDelete("{position}")]
        public IActionResult SupprimerForme(string position, [FromQuery] int boardId = 1)
        {
            bool deleted = _gameManager.SupprimerForme(position, boardId);
            if (!deleted)
            {
                _logger.LogWarning($"Aucune forme supprimée à la position {position} sur l'échiquier {boardId}.");
                return NotFound($"Aucune forme trouvée en {position} à supprimer sur l'échiquier {boardId}.");
            }
            _logger.LogInformation($"Forme supprimée avec succès de la position {position} sur l'échiquier {boardId}.");
            return Ok($"Forme supprimée de la position {position} sur l'échiquier {boardId}");
        }

        [HttpPut("{position}")]
        public IActionResult ModifierForme(string position, [FromBody] FormeDto formeDto, [FromQuery] int boardId = 1)
        {
            if (formeDto == null)
            {
                _logger.LogWarning("Tentative de mise à jour avec des données nulles.");
                return BadRequest("Données invalides");
            }

            Forme nouvelleForme = CreerFormeDepuisDto(formeDto, position);
            if (nouvelleForme == null)
            {
                _logger.LogWarning($"Type de forme inconnu: {formeDto.Type}");
                return BadRequest("Type de forme inconnu");
            }

            bool modified = _gameManager.ModifierForme(position, nouvelleForme, boardId);
            if (!modified)
            {
                _logger.LogWarning($"Échec de la modification à la position {position} sur l'échiquier {boardId}.");
                return NotFound($"La position {position} n'existe pas ou n'a pas pu être modifiée sur l'échiquier {boardId}.");
            }
            _logger.LogInformation($"Forme à la position {position} mise à jour avec {nouvelleForme.GetType().Name} sur l'échiquier {boardId}.");
            return Ok($"La position {position} a été mise à jour sur l'échiquier {boardId}.");
        }

        [HttpGet("board")]
        public IActionResult ObtenirEchiquier([FromQuery] int boardId = 1)
        {
            var echiquier = _gameManager.ObtenirEchiquier(boardId);
            _logger.LogInformation($"Affichage de l'échiquier {boardId} demandé.");
            return Ok(echiquier);
        }

        [HttpGet("forme/{position}")]
        public IActionResult ObtenirDetailsForme(string position, [FromQuery] int boardId = 1)
        {
            var forme = _gameManager.ObtenirDetailsForme(position, boardId);
            if (forme == null)
            {
                _logger.LogWarning($"Aucune forme trouvée à la position {position} sur l'échiquier {boardId}.");
                return NotFound($"Aucune forme à la position {position} sur l'échiquier {boardId}.");
            }
            
            _logger.LogInformation($"Détails de la forme à la position {position} sur l'échiquier {boardId} demandés.");
            
            // Créer un objet anonyme avec les propriétés de la forme selon son type
            if (forme is Carre carre)
            {
                return Ok(new { Cote = carre.Cote });
            }
            else if (forme is Rectangle rectangle)
            {
                return Ok(new { Longueur = rectangle.Longueur, Largeur = rectangle.Largeur });
            }
            else if (forme is Triangle triangle)
            {
                return Ok(new { Cote = triangle.Cote });
            }
            else if (forme is Cercle cercle)
            {
                return Ok(new { Rayon = cercle.Rayon });
            }
            
            return Ok(new {});
        }

        [HttpGet("boards")]
        public IActionResult GetAllBoards()
        {
            var boards = _gameManager.GetAllBoards();
            return Ok(boards);
        }

        [HttpPost("board")]
        public IActionResult CreateBoard([FromBody] BoardDto boardDto)
        {
            if (string.IsNullOrEmpty(boardDto.Name))
            {
                return BadRequest("Le nom de l'échiquier est requis");
            }

            var board = _gameManager.CreateBoard(boardDto.Name, boardDto.Type ?? "standard");
            return Ok(board);
        }

        private Forme CreerFormeDepuisDto(FormeDto formeDto, string position)
        {
            switch (formeDto.Type.ToLower())
            {
                case "carre":
                    return new Carre(formeDto.Cote, position);
                case "rectangle":
                    return new Rectangle(formeDto.Longueur, formeDto.Largeur, position);
                case "triangle":
                    return new Triangle(formeDto.Cote, position);
                case "cercle":
                    return new Cercle(formeDto.Rayon, position);
                default:
                    return null;
            }
        }
    }

    // Ajout de cette classe DTO pour créer un échiquier
    public class BoardDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}