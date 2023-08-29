using Microsoft.AspNetCore.Mvc;
using Tibis.Facade.Web.Models;

namespace Tibis.Facade.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class FacadeController : ControllerBase
{
    private readonly IFacadeService _facadeService;

    private readonly ILogger<FacadeController> _logger;

    public FacadeController(ILogger<FacadeController> logger, IFacadeService facadeService)
    {
        _logger = logger;
        _facadeService = facadeService;
    }

    [HttpPost]
    [Route("CreateDemoData")]
    public async Task<ActionResult<DemoDataDto>> CreateDemoData()
    {
        _logger.LogInformation("Creating demo data");
        var result = await _facadeService.CreateDemoDataAsync();
        return Ok(result);
    }

    [HttpPost]
    [Route("CreateInvalidSubscription")]
    public async Task<ActionResult> CreateInvalidSubscription()
    {
        _logger.LogInformation("Creating invalid subscription");
        await _facadeService.CreateInvalidSubscriptionAsync();
        return Ok();
    }
}