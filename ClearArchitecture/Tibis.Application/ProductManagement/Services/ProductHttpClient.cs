using Microsoft.Extensions.Configuration;
using Tibis.Application.HttpClients;
using Tibis.Domain;
using Tibis.Domain.ProductManagement;

namespace Tibis.Application.ProductManagement.Services;

public class ProductHttpClient : IProductClient
{
    private readonly ProductManagementHttpClient _httpClient;

    public ProductHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        var url = configuration["ProductManagementUrl"]
                  ?? throw new InvalidConfigurationException("ProductManagementUrl");
        _httpClient = new(url, httpClient);
    }

    public async Task<Product> GetProductAsync(Guid id)
    {
        var product = await _httpClient.ProductGETAsync(id);
        return new(product.Id, product.Name, (ProductType)product.ProductType, product.Rate);
    }

    public async Task<Product> GetProductAsync(string name)
    {
        var product = await _httpClient.NameAsync(name);
        return new(product.Id, product.Name, (ProductType)product.ProductType, product.Rate);
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        var createdProduct = await _httpClient.ProductPOSTAsync(
            new() { Name = product.Name, ProductType = (int)product.ProductType, Rate = product.Rate});
        return new(createdProduct.Id, createdProduct.Name, (ProductType)createdProduct.ProductType, createdProduct.Rate);
    }
}