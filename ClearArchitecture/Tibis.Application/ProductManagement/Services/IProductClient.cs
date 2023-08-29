using Tibis.Domain.ProductManagement;

namespace Tibis.Application.ProductManagement.Services;

public interface IProductClient
{
    Task<Product> GetProductAsync(Guid id);
    Task<Product> CreateProductAsync(Product product);
}