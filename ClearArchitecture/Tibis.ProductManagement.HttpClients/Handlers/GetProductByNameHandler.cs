using MediatR;
using Tibis.ProductManagement.CQRS.Models;
using Tibis.ProductManagement.CQRS.Requests;

namespace Tibis.ProductManagement.HttpClients.Handlers;

internal class GetProductByNameHandler: IRequestHandler<GetProductByNameRequest, ProductDto>
{
    private readonly IProductManagementClient _client;

    public GetProductByNameHandler(IProductManagementClient client) => _client = client;

    public async Task<ProductDto> Handle(GetProductByNameRequest request, CancellationToken cancellationToken) => 
        await _client.GetProductAsync(request.Name, cancellationToken);
}