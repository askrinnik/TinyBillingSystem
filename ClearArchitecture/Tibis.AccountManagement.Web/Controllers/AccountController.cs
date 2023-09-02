using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tibis.AccountManagement.CQRS.Models;
using Tibis.AccountManagement.CQRS.Requests;

namespace Tibis.AccountManagement.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly ISender _sender;

    public AccountController(ILogger<AccountController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IEnumerable<AccountDto>>> GetAll()
    {
        _logger.LogInformation("Getting all accounts");
        return Ok(await _sender.Send(new GetAllAccountsRequest()));
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<AccountDto>> Get(Guid id)
    {
        _logger.LogInformation("Getting account with id '{Id}'", id);
        return Ok(await _sender.Send(new GetAccountByIdRequest(id)));
    }


    [HttpGet]
    [Route("name/{name}")]
    public async Task<ActionResult<AccountDto>> GetByName(string name)
    {
        _logger.LogInformation("Getting account with name '{Name}'", name);
        return Ok(await _sender.Send(new GetAccountByNameRequest(name)));
    }

    [HttpPost]
    public async Task<ActionResult<AccountDto>> Create(CreateAccountRequest request)
    {
        _logger.LogInformation("Creating account with name '{Name}'", request.Name);
        return Ok(await _sender.Send(request));
    }
}