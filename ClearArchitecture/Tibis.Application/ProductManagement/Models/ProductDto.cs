using Tibis.Domain.ProductManagement;

namespace Tibis.Application.ProductManagement.Models;

public record ProductDto(Guid Id, string Name, int ProductType, int Rate)
{
    public static ProductDto From(Product account) => new(account.Id, account.Name, (int)account.ProductType, account.Rate);
}