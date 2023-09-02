namespace Tibis.ProductManagement.CQRS.Models;

public record ProductDto(Guid Id, string Name, ProductType ProductType, int Rate);