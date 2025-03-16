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

        public HomeApiController(ILogger<HomeApiController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("getcarre")]
        public IActionResult GetCarre()
        {
            // Exemple de création d'un objet Carre
            var carre = new Carre(5);

            // Mapper l'objet Carre vers le CarreDto
            var carreDto = _mapper.Map<CarreDto>(carre);

            // Retourner l'objet DTO dans la réponse API
            return Ok(carreDto);
        }

        [HttpGet("getrectangle")]
        public IActionResult GetRectangle()
        {
            var rectangle = new Rectangle(6, 4);
            var rectangleDto = _mapper.Map<RectangleDto>(rectangle);
            return Ok(rectangleDto);
        }

        [HttpGet("getcercle")]
        public IActionResult GetCercle()
        {
            var cercle = new Cercle(3);
            var cercleDto = _mapper.Map<CercleDto>(cercle);
            return Ok(cercleDto);
        }
        [HttpGet("cercle/{rayon}")]
        public IActionResult GetCercleWithParam(double rayon)
        {
            var cercle = new Cercle(rayon);
            var cercleDto = _mapper.Map<CercleDto>(cercle);
            return Ok(cercleDto);
        }

        [HttpGet("gettriangle")]
        public IActionResult GetTriangle()
        {
            var triangle = new Triangle(5);
            var triangleDto = _mapper.Map<TriangleDto>(triangle);
            return Ok(triangleDto);
        }
    }
}