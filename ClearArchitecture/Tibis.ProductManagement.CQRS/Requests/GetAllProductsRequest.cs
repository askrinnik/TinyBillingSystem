using MediatR;
using Tibis.ProductManagement.CQRS.Models;

namespace Tibis.ProductManagement.CQRS.Requests;

public record GetAllProductsRequest : IRequest<IEnumerable<ProductDto>>;