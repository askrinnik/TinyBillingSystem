using Tibis.ProductManagement.CQRS.Models;
using Tibis.ProductManagement.Domain;

namespace Tibis.ProductManagement.Application;

internal static class DtoExtensions
{
    public static ProductDto ToDto(this Product product) => new(product.Id, product.Name, (CQRS.Models.ProductType)product.ProductType, product.Rate);
}