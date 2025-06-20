using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.Data;
using Shared.forme;
using WebAPI.Models.DTOs;



namespace WebAPI.Controllers
{
    [ApiController]
    [Route("Formes")]
    public class HomeApiController : ControllerBase
    {
        private readonly ILogger<HomeApiController> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public HomeApiController(ILogger<HomeApiController> logger, IMapper mapper, ApplicationDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetForms()
        {
            var forms = _context.Formes.ToList();
            var formsDto = forms.Select(f => _mapper.Map<FormeDto>(f)).ToList();
            _logger.LogInformation("Liste des formes récupérée.");
            return Ok(formsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetFormById(Guid id)
        {
            var forme = _context.Formes.FirstOrDefault(f => f.Id == id);
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
            _context.Formes.Add(forme);
            _context.SaveChanges();
            _logger.LogInformation($"Forme de type {forme.GetType().Name} créée avec ID {forme.Id}.");
            return CreatedAtAction(nameof(GetFormById), new { id = forme.Id }, _mapper.Map<FormeDto>(forme));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateForm(Guid id, [FromBody] FormeDto formeDto)
        {
            var existingForm = _context.Formes.FirstOrDefault(f => f.Id == id);
            if (existingForm == null)
            {
                _logger.LogWarning($"Tentative de mise à jour d'une forme inexistante (ID: {id}).");
                return NotFound();
            }

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
            _context.SaveChanges();
            _logger.LogInformation($"Forme avec ID {id} mise à jour.");
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteForm(Guid id)
        {
            var forme = _context.Formes.FirstOrDefault(f => f.Id == id);
            if (forme == null)
            {
                var existingIds = string.Join(", ", _context.Formes.Select(f => f.Id));
                _logger.LogWarning($"Tentative de suppression d'une forme inexistante (ID: {id}). IDs existants: {existingIds}");
                return NotFound();
            }
            _context.Formes.Remove(forme);
            _context.SaveChanges();
            _logger.LogInformation($"Forme avec ID {id} supprimée.");
            return NoContent();
        }
    }
}
