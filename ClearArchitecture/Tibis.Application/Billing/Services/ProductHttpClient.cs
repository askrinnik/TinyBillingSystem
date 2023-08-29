using Microsoft.Extensions.Configuration;
using Tibis.Application.HttpClients;

namespace Tibis.Application.Billing.Services;

public class ProductHttpClient: IProductClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ProductHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<ProductManagement.Models.ProductDto> GetProductAsync(Guid id)
    {
        var productManagementUrl = _configuration["ProductManagementUrl"] ?? "http://localhost:5002";
        var prodClient = new ProductManagementHttpClient(productManagementUrl, _httpClient);
        var product = await prodClient.ProductGETAsync(id);
        return new(product.Id, product.Name, product.ProductType, product.Rate);
    }
}