using MediatR;
using Tibis.ProductManagement.CQRS.Models;

namespace Tibis.ProductManagement.CQRS.Requests;

public record CreateProductRequest(string Name, ProductType ProductType, int Rate) : IRequest<ProductDto>;