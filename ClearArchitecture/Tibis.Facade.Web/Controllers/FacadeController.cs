using Microsoft.AspNetCore.Mvc;

namespace Tibis.Facade.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class FacadeController : ControllerBase
{
    private readonly IFacadeHttpClient _facadeHttpClient;

    private readonly ILogger<FacadeController> _logger;

    public FacadeController(ILogger<FacadeController> logger, IFacadeHttpClient facadeHttpClient)
    {
        _logger = logger;
        _facadeHttpClient = facadeHttpClient;
    }

    [HttpPost(Name = "CreateDemoData")]
    public async Task<IActionResult> CreateDemoData()
    {
        _logger.LogInformation("Creating demo data");
        await _facadeHttpClient.CreateDemoDataAsync();
        return Ok();
    }
}