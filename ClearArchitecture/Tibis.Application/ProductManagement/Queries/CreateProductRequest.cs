using MediatR;
using Tibis.Application.ProductManagement.Models;

namespace Tibis.Application.ProductManagement.Queries
{
    public record CreateProductRequest(string Name) : IRequest<ProductDto>;
}