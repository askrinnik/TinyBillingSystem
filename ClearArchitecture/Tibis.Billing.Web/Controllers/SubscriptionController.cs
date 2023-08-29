using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tibis.Application.Billing.Models;
using Tibis.Application.Billing.Queries;

namespace Tibis.Billing.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly ILogger<SubscriptionController> _logger;
    private readonly ISender _sender;

    public SubscriptionController(ILogger<SubscriptionController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IEnumerable<SubscriptionDto>>> GetAll()
    {
        _logger.LogInformation("Getting all Subscriptions");
        return Ok(await _sender.Send(new GetAllSubscriptionsRequest()));
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<SubscriptionDto>> Get(Guid id)
    {
        _logger.LogInformation("Getting Subscription with id '{Id}'", id);
        return Ok(await _sender.Send(new GetSubscriptionByIdRequest(id)));
    }

    [HttpPost]
    public async Task<ActionResult<SubscriptionDto>> Create(CreateSubscriptionRequest request)
    {
        _logger.LogInformation("Creating Subscription for an account '{AccountId}' to a product '{ProductId}'", 
            request.AccountId, request.ProductId);
        return Ok(await _sender.Send(request));
    }
}