using Tibis.Domain.ProductManagement;

namespace Tibis.Application.ProductManagement.Models;

public record ProductDto(Guid Id, string Name)
{
    public static ProductDto From(Product account) => new(account.Id, account.Name);
}