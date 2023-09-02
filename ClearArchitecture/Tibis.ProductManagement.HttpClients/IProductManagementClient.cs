using Tibis.ProductManagement.CQRS.Models;
using Tibis.ProductManagement.CQRS.Requests;

namespace Tibis.ProductManagement.HttpClients;

internal interface IProductManagementClient
{
    Task<ProductDto> GetProductAsync(Guid id, CancellationToken cancellationToken);
    Task<ProductDto> GetProductAsync(string name, CancellationToken cancellationToken);
    Task<ProductDto> CreateProductAsync(CreateProductRequest product, CancellationToken cancellationToken);
    Task<IEnumerable<ProductDto>> GetAllProductsAsync(CancellationToken cancellationToken);
}