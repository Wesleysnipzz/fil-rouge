using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EzChess.forme;
using WebAPI.Models.DTOs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("Formes")]
    public class HomeApiController : ControllerBase
    {
        private readonly ILogger<HomeApiController> _logger;
        private readonly IMapper _mapper;
        // Liste statique afin de conserver les formes entre chaque requête
        private static readonly List<Forme> _formes = new List<Forme>();
        // Instance statique de GameManager pour gérer l'échiquier
        private static readonly EzChess.GameManager _gameManager = new EzChess.GameManager();

        public HomeApiController(ILogger<HomeApiController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;

            // Initialisation uniquement si la liste est vide
            if (!_formes.Any())
            {
                _formes.Add(new Carre(5));
                _formes.Add(new Rectangle(6, 4));
                _formes.Add(new Cercle(3));
                _formes.Add(new Triangle(5));
            }
        }

        [HttpGet]
        public IActionResult GetForms()
        {
            var formsDto = _formes.Select(f => _mapper.Map<FormeDto>(f)).ToList();
            _logger.LogInformation("Liste des formes récupérée.");
            return Ok(formsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetFormById(Guid id)
        {
            var forme = _formes.FirstOrDefault(f => f.Id == id);
            if (forme == null)
            {
                _logger.LogWarning($"Forme avec ID {id} non trouvée.");
                return NotFound();
            }
            _logger.LogInformation($"Forme avec ID {id} récupérée.");
            return Ok(_mapper.Map<FormeDto>(forme));
        }

        [HttpPost]
        public IActionResult CreateForm([FromBody] FormeDto formeDto)
        {
            if (formeDto == null)
            {
                _logger.LogWarning("Tentative de création d'une forme avec des données nulles.");
                return BadRequest("Données invalides");
            }

            Forme forme;
            switch (formeDto.Type.ToLower())
            {
                case "carre":
                    forme = new Carre(formeDto.Cote);
                    break;
                case "rectangle":
                    forme = new Rectangle(formeDto.Longueur, formeDto.Largeur);
                    break;
                case "triangle":
                    forme = new Triangle(formeDto.Cote);
                    break;
                case "cercle":
                    forme = new Cercle(formeDto.Rayon);
                    break;
                default:
                    _logger.LogWarning($"Type de forme inconnu: {formeDto.Type}");
                    return BadRequest("Type de forme inconnu");
            }
            _formes.Add(forme);
            _logger.LogInformation($"Forme de type {forme.GetType().Name} créée avec ID {forme.Id}.");
            return CreatedAtAction(nameof(GetFormById), new { id = forme.Id }, _mapper.Map<FormeDto>(forme));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateForm(Guid id, [FromBody] FormeDto formeDto)
        {
            var existingForm = _formes.FirstOrDefault(f => f.Id == id);
            if (existingForm == null)
            {
                _logger.LogWarning($"Tentative de mise à jour d'une forme inexistante (ID: {id}).");
                return NotFound();
            }

            // Mise à jour selon le type de la forme existante
            switch (existingForm)
            {
                case Carre carre when formeDto.Type.ToLower() == "carre":
                    carre.Cote = formeDto.Cote;
                    break;
                case Rectangle rect when formeDto.Type.ToLower() == "rectangle":
                    rect.Longueur = formeDto.Longueur;
                    rect.Largeur = formeDto.Largeur;
                    break;
                case Triangle triangle when formeDto.Type.ToLower() == "triangle":
                    triangle.Cote = formeDto.Cote;
                    break;
                case Cercle cercle when formeDto.Type.ToLower() == "cercle":
                    cercle.Rayon = formeDto.Rayon;
                    break;
                default:
                    _logger.LogWarning($"Type mismatch lors de la mise à jour pour l'ID {id}.");
                    return BadRequest("Le type de forme ne correspond pas");
            }
            _logger.LogInformation($"Forme avec ID {id} mise à jour.");
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteForm(Guid id)
        {
            var forme = _formes.FirstOrDefault(f => f.Id == id);
            if (forme == null)
            {
                // Log de débogage avec les IDs existants
                var existingIds = string.Join(", ", _formes.Select(f => f.Id));
                _logger.LogWarning($"Tentative de suppression d'une forme inexistante (ID: {id}). IDs existants: {existingIds}");
                return NotFound();
            }
            _formes.Remove(forme);
            _logger.LogInformation($"Forme avec ID {id} supprimée.");
            return NoContent();
        }
    }
}
