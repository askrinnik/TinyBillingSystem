using Tibis.Application.ProductManagement.Models;

namespace Tibis.Application.Billing.Services;

public interface IProductClient
{
    Task<ProductDto> GetProductAsync(Guid id);
}