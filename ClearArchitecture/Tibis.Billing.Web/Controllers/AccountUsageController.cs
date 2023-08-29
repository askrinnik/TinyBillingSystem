using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tibis.Application.Billing.Models;
using Tibis.Application.Billing.Queries;

namespace Tibis.Billing.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountUsageController : ControllerBase
{
    private readonly ILogger<SubscriptionController> _logger;
    private readonly ISender _sender;

    public AccountUsageController(ILogger<SubscriptionController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IEnumerable<AccountUsageDto>>> GetAll()
    {
        _logger.LogInformation("Getting all account usages");
        return Ok(await _sender.Send(new GetAllAccountUsagesRequest()));
    }
}