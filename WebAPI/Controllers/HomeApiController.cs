using Microsoft.AspNetCore.Mvc;
namespace WebApplication1.Controllers;

[ApiController]
[Route("api/home")]
public class HomeApiController : ControllerBase
{
    private readonly ILogger<HomeApiController> _logger;

    public HomeApiController(ILogger<HomeApiController> logger)
    {
        _logger = logger;
    }

    [HttpGet("helloflorent")]
    public IActionResult Get()
    {
        return Ok("Hello Florent");
    }
}