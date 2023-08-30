using MediatR;
using Tibis.Application.ProductManagement.Models;

namespace Tibis.Application.ProductManagement.Queries;

public record GetProductByNameRequest(string Name) : IRequest<ProductDto>;