using MediatR;
using Tibis.ProductManagement.CQRS.Models;
using Tibis.ProductManagement.CQRS.Requests;

namespace Tibis.ProductManagement.HttpClients.Handlers;

internal class GetAllProductsHandler : IRequestHandler<GetAllProductsRequest, IEnumerable<ProductDto>>
{
    private readonly IProductManagementClient _client;

    public GetAllProductsHandler(IProductManagementClient client) => _client = client;

    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken) =>
        await _client.GetAllProductsAsync(cancellationToken);
}