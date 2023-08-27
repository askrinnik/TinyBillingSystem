using MediatR;
using Tibis.Application.ProductManagement.Models;

namespace Tibis.Application.ProductManagement.Queries;

public record GetProductByIdRequest(Guid Id) : IRequest<ProductDto>;