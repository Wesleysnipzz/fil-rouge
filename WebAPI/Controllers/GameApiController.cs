using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.DTOs;
using Shared.forme;
using Shared.Data;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameApiController : ControllerBase
    {
        private readonly ILogger<GameApiController> _logger;
        private readonly GameManager _gameManager;

        public GameApiController(ILogger<GameApiController> logger, GameManager gameManager)
        {
            _logger = logger;
            _gameManager = gameManager;
        }

        [HttpPost("{position}")]
        public IActionResult PlacerForme(string position, [FromBody] FormeDto formeDto)
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

            var placed = _gameManager.PlacerForme(position, forme);
            if (!placed)
            {
                _logger.LogWarning($"Échec du placement de la forme à la position {position}.");
                return BadRequest($"Échec du placement sur l'échiquier en {position}");
            }

            _logger.LogInformation($"Forme enregistrée à la position {position}.");
            return Ok("Placement réussi");
        }

        [HttpDelete("{position}")]
        public IActionResult SupprimerForme(string position)
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
        public IActionResult ModifierForme(string position, [FromBody] FormeDto formeDto)
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
        public IActionResult ObtenirEchiquier()
        {
            var echiquier = _gameManager.ObtenirEchiquier();
            _logger.LogInformation("Affichage de l'échiquier demandé.");
            return Ok(echiquier);
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
}
