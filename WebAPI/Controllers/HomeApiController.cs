// Langage: csharp
// Fichier: WebAPI/Controllers/HomeApiController.cs
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EzChess.forme;
using WebAPI.Models.DTOs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeApiController : ControllerBase
    {
        private readonly ILogger<HomeApiController> _logger;
        private readonly IMapper _mapper;
        // Liste statique afin de conserver les formes entre chaque requête
        private static readonly List<Forme> _formes = new List<Forme>();

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
            var formsDto = _formes.Select(f => new FormeDto(f)).ToList();
            return Ok(formsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetFormById(Guid id)
        {
            var forme = _formes.FirstOrDefault(f => f.Id == id);
            if (forme == null)
                return NotFound();
            return Ok(new FormeDto(forme));
        }

        [HttpPost]
        public IActionResult CreateForm([FromBody] FormeDto formeDto)
        {
            if (formeDto == null)
                return BadRequest("Données invalides");

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
                    return BadRequest("Type de forme inconnu");
            }
            _formes.Add(forme);
            return CreatedAtAction(nameof(GetFormById), new { id = forme.Id }, new FormeDto(forme));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateForm(Guid id, [FromBody] FormeDto formeDto)
        {
            var existingForm = _formes.FirstOrDefault(f => f.Id == id);
            if (existingForm == null)
                return NotFound();

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
                    return BadRequest("Le type de forme ne correspond pas");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteForm(Guid id)
        {
            var forme = _formes.FirstOrDefault(f => f.Id == id);
            if (forme == null)
                return NotFound();

            _formes.Remove(forme);
            return NoContent();
        }
    }
}