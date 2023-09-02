using Microsoft.Extensions.Configuration;
using Tibis.Contracts.Exceptions;
using Tibis.ProductManagement.CQRS.Models;
using Tibis.ProductManagement.CQRS.Requests;

namespace Tibis.ProductManagement.HttpClients;

internal class ProductHttpClient : IProductManagementClient
{
    private readonly OpenApi.ProductManagementHttpClient _httpClient;

    public ProductHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        var url = configuration["ProductManagementUrl"]
                  ?? throw new InvalidConfigurationException("ProductManagementUrl");
        _httpClient = new(url, httpClient);
    }

    public async Task<ProductDto> GetProductAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await _httpClient.ProductGETAsync(id, cancellationToken);
        return new(product.Id, product.Name, (ProductType)product.ProductType, product.Rate);
    }

    public async Task<ProductDto> GetProductAsync(string name, CancellationToken cancellationToken)
    {
        var product = await _httpClient.NameAsync(name, cancellationToken);
        return new(product.Id, product.Name, (ProductType)product.ProductType, product.Rate);
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductRequest product, CancellationToken cancellationToken)
    {
        var createdProduct = await _httpClient.ProductPOSTAsync(
            new() { Name = product.Name, ProductType = (OpenApi.ProductType)product.ProductType, Rate = product.Rate}, cancellationToken);
        return new(createdProduct.Id, createdProduct.Name, (ProductType)createdProduct.ProductType, createdProduct.Rate);
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(CancellationToken cancellationToken) => 
        (await _httpClient.AllAsync(cancellationToken))
        .Select(item => new ProductDto(item.Id, item.Name, (ProductType)item.ProductType, item.Rate));
}