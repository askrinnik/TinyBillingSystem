using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tibis.Application.ProductManagement.Models;
using Tibis.Application.ProductManagement.Queries;

namespace Tibis.ProductManagement.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly ISender _sender;

    public ProductController(ILogger<ProductController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        _logger.LogInformation("Getting all products");
        return Ok(await _sender.Send(new GetAllProductsRequest()));
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<ProductDto>> Get(Guid id)
    {
        _logger.LogInformation("Getting product with id '{Id}'", id);
        return Ok(await _sender.Send(new GetProductByIdRequest(id)));
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create(CreateProductRequest request)
    {
        _logger.LogInformation("Creating Product with name '{Name}'", request.Name);
        return Ok(await _sender.Send(request));
    }
}