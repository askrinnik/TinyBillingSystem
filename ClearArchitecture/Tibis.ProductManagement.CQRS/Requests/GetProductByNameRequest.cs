using MediatR;
using Tibis.ProductManagement.CQRS.Models;

namespace Tibis.ProductManagement.CQRS.Requests;

public record GetProductByNameRequest(string Name) : IRequest<ProductDto>;