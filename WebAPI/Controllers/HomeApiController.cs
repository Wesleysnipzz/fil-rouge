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
    }
}