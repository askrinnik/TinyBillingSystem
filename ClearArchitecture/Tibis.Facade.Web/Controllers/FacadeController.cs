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
    [Route("CreateRcSubscription")]
    public async Task<ActionResult<DemoDataDto>> CreateRcSubscription()
    {
        _logger.LogInformation("Creating an RC subscription");
        var result = await _facadeService.CreateRcSubscriptionAsync();
        return Ok(result);
    }

    [HttpPost]
    [Route("CreateUsageSubscription")]
    public async Task<ActionResult<DemoDataDto>> CreateUsageSubscription()
    {
        _logger.LogInformation("Creating a usage subscription and meter");
        var result = await _facadeService.CreateUsageSubscriptionAsync();
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